using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.AspNetCore
{
    public class PrefixRoute : IRouter
    {
        private readonly IRouteHandler _target;
        private readonly string _prefix;

        public PrefixRoute(IRouteHandler target, string prefix)
        {
            _target = target;

            if (prefix == null)
            {
                prefix = "/";
            }
            else if (prefix.Length > 0 && prefix[0] != '/')
            {
                // owin.RequestPath starts with a /
                prefix = "/" + prefix;
            }

            if (prefix.Length > 1 && prefix[prefix.Length - 1] == '/')
            {
                prefix = prefix.Substring(0, prefix.Length - 1);
            }

            _prefix = prefix;
        }

        public Task RouteAsync(RouteContext context)
        {
            var requestPath = context.HttpContext.Request.Path.Value ?? string.Empty;
            if (requestPath.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
            {
                if (requestPath.Length > _prefix.Length)
                {
                    var lastCharacter = requestPath[_prefix.Length];
                    if (lastCharacter != '/' && lastCharacter != '#' && lastCharacter != '?')
                    {
                        return Task.FromResult(0);
                    }
                }

                context.Handler = _target.GetRequestHandler(context.HttpContext, context.RouteData);
            }

            return Task.FromResult(0);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }
    }
}
