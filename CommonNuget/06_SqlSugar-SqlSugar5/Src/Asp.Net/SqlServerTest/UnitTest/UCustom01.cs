﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmTest
{
    public class UCustom01
    {

        public static void Init()
        {
            var db = new SqlSugarScope(new SqlSugar.ConnectionConfig()
            {
                ConnectionString = Config.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });
            db.Aop.OnLogExecuted = (s, p) =>
            {
                Console.WriteLine(s);
            };
            //建表 
            if (!db.DbMaintenance.IsAnyTable("User_Test001", false))
            {
                db.CodeFirst.InitTables<User_Test001>();
            }
            if (!db.DbMaintenance.IsAnyTable("UserRole_Test001", false))
            {
                db.CodeFirst.InitTables<UserRole_Test001>();
            }

            //用例代码 
            var result = db.Queryable<User_Test001, UserRole_Test001>((u, ur) => new object[] {

                            JoinType.Left,u.ID==ur.UserID



                        }).Select((u, ur) => new

                        {

                            customName = SqlFunc.Subqueryable<User_Test001>().Where(s => s.UserName == u.UserName).Select(s => s.UserName+"")



                        }).ToPageList(1, 10);
            db.CodeFirst.InitTables<BoolTest12313>();
            var x1 = db.Queryable<BoolTest12313>().Where(it => it.IsDeleted && SqlFunc.HasValue(it.name) ).ToList();
            var x2 = db.Queryable<BoolTest12313>().Where(it => it.IsDeleted && SqlFunc.HasValue(it.name)==true).ToList();
            var x3 = db.Queryable<BoolTest12313>().Where(it =>true  && SqlFunc.HasValue(it.name) == true).ToList();
            var x4 = db.Queryable<BoolTest12313>().Where(it =>SqlFunc.HasValue(it.name) == true).ToList();
            var x5 = db.Queryable<BoolTest12313>().Where(it => SqlFunc.HasValue(it.name) ).ToList();
            var x6 = db.Queryable<BoolTest12313>().Select(it => new  { x=SqlFunc.HasValue(it.name)}).ToList();
            db.DbMaintenance.DropTable("BoolTest12313");
        }

        public class BoolTest12313
        {
            public string name { get; set; }
            public bool IsDeleted { get; set; }
            public int xx { get; set; }
        }

        [SugarTable("unitUser_Test001")]
        public class User_Test001
        {

            public int ID { get; set; }
            public string UserName { get; set; }
        }
        [SugarTable("unitUserRole_Test001")]
        public class UserRole_Test001
        {

            public int ID { get; set; }
            public int UserID { get; set; }
        }
    }
}
