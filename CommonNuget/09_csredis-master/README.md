## Features

- CSRedisClient and RedisHelper Keep all method names consistent with redis-cli

- Support geo type commands (redis-server 3.2 or above is required)

- Support Redis Cluster redis-trib.rb

- Support Redis Sentinel and master-slave

- Supports stream type commands (requires redis-server 5.0 and above)

| Package Name |  NuGet | Downloads | |
|--------------|  ------- |  ---- | -- |
| CSRedisCore | [![nuget](https://img.shields.io/nuget/v/CSRedisCore.svg?style=flat-square)](https://www.nuget.org/packages/CSRedisCore) | [![stats](https://img.shields.io/nuget/dt/CSRedisCore.svg?style=flat-square)](https://www.nuget.org/stats/packages/CSRedisCore?groupby=Version) |
| Caching.CSRedis | [![nuget](https://img.shields.io/nuget/v/Caching.CSRedis.svg?style=flat-square)](https://www.nuget.org/packages/Caching.CSRedis) | [![stats](https://img.shields.io/nuget/dt/Caching.CSRedis.svg?style=flat-square)](https://www.nuget.org/stats/packages/Caching.CSRedis?groupby=Version) | IDistributedCache |

> dotnet add package CSRedisCore

## Single machine redis

```csharp
var csredis = new CSRedis.CSRedisClient("127.0.0.1:6379,password=123,defaultDatabase=13,prefix=my_");
```

| Parameter         | Default   | Explain |
| :---------------- | --------: | :------------------- |
| user              | \<Empty\> | Redis server user (redis 6.0+) |
| password          | \<Empty\> | Redis server password |
| defaultDatabase   | 0         | Redis server database |
| **asyncPipeline** | false     | The asynchronous method automatically uses pipeline, and the 10W concurrent time is 450ms (welcome to feedback) |
| poolsize          | 50        | Connection pool size |
| idleTimeout       | 20000     | Idle time of elements in the connection pool (MS), suitable for connecting to remote redis server |
| connectTimeout    | 5000      | Connection timeout (MS) |
| syncTimeout       | 10000     | Send / receive timeout (MS) |
| preheat           | 5         | Preheat connections, receive values such as preheat = 5 preheat 5 connections |
| autoDispose       | true      | Follow system exit event to release automatically |
| ssl               | false     | Enable encrypted transmission |
| testcluster       | true      | ?????????????????????????????????????????????????????????????????????????????? false |
| tryit             | 0         | Execution error, retry attempts |
| name              | \<Empty\> | Connection name, use client list command to view |
| prefix            | \<Empty\> | key?????????????????????????????????????????????csredis.Set(prefix + "key", 111); |

> IPv6: [fe80::b164:55b3:4b4f:7ce6%15]:6379

# Redis Sentinel

```csharp
var csredis = new CSRedis.CSRedisClient("mymaster,password=123,prefix=my_", 
  new [] { "192.169.1.10:26379", "192.169.1.11:26379", "192.169.1.12:26379" });
```

Read only: new CSRedisClient("mymaster,password=123", new [] { Sentinels }, false)

# Redis Cluster

???????????????????????? redis-trib ?????????????????????????????????????????? CSRedisClient ????????????????????? redis-server ????????? MOVED | ASK ????????????slot????????????????????? Nodes ?????????

> 127.0.0.1:6379,password=123,defaultDatabase=0,poolsize=50,prefix=

> ??????????????????????????????????????????????????????????????????????????????

??????????????????????????????????????????????????????????????????????????????prefix=key???????????????????????????????????????????????????????????? keySlot ???????????????????????????????????????????????? slotCache???

> ????????????????????????????????? keys ???????????????????????????Eval???????????????????????????????????????

# IDistributedCache

> dotnet add package Caching.CSRedis

```csharp
RedisHelper.Initialization(csredis);
services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
```

> Note: CSRedisClient is singleton, RedisHelper static class is recommended

```csharp
RedisHelper.Set("test1", "123123", 60);
RedisHelper.Get("test1");
//The method name is the same as the command of redis cli
```

# Operate on multiple databases

```csharp
var connectionString = "127.0.0.1:6379,password=123,poolsize=10";
var redis = new CSRedisClient[14]; //Singleton
for (var a = 0; a< redis.Length; a++) 
  redis[a] = new CSRedisClient(connectionString + ",defaultDatabase=" + a);

redis[1].Get("test1");
```

> Multiple RedisHelper

```csharp
public abstract class MyHelper1 : RedisHelper<MyHelper1> {}
public abstract class MyHelper2 : RedisHelper<MyHelper2> {}

MyHelper1.Initialization(new CSRedisClient("...."));
MyHelper2.Initialization(new CSRedisClient("...."));
```

# Subscribe/Publish

```csharp
//Native subscribe
RedisHelper.Subscribe(
  ("chan1", msg => Console.WriteLine(msg.Body)),
  ("chan2", msg => Console.WriteLine(msg.Body)));

RedisHelper.PSubscribe(new[] { "test*", "*test001", "test*002" }, msg => {
  Console.WriteLine($"PSUB   {msg.MessageId}:{msg.Body}    {msg.Pattern}: chan:{msg.Channel}");
});

//????????????????????????????????????
//1???????????????????????????????????????????????????????????????????????????????????????????????????????????????
//2????????? "test*", "*test001", "test*002" ?????????????????????????????????????????????????????????????????????

RedisHelper.Publish("chan1", "123123123");
```

???????????????[??????????????????redis ?????????????????????????????????](https://www.cnblogs.com/kellynic/p/9952386.html)

# CacheShell

```csharp
//?????????????????????????????????????????????
var t1 = Test.Select.WhereId(1).ToOne();

//???????????????????????????????????????????????????
var cacheValue = RedisHelper.Get("test1");
if (!string.IsNullOrEmpty(cacheValue)) {
	try {
		return JsonConvert.DeserializeObject(cacheValue);
	} catch {
		//???????????????key
		RedisHelper.Remove("test1");
		throw;
	}
}
var t1 = Test.Select.WhereId(1).ToOne();
RedisHelper.Set("test1", JsonConvert.SerializeObject(t1), 10); //??????10???

//???????????????????????????????????????????????? string ??? hash ????????????
var t1 = RedisHelper.CacheShell("test1", 10, () => Test.Select.WhereId(1).ToOne());
var t2 = RedisHelper.CacheShell("test", "1", 10, () => Test.Select.WhereId(1).ToOne());
var t3 = RedisHelper.CacheShell("test", new [] { "1", "2" }, 10, notCacheFields => new [] {
  ("1", Test.Select.WhereId(1).ToOne()),
  ("2", Test.Select.WhereId(2).ToOne())
});
```

# Pipeline

???????????????????????????????????????????????????????????????????????????

```csharp
var ret1 = RedisHelper.StartPipe(p => p.Set("a", "1").Get("a"));
```

# Benchmark

100,000 operations

```shell
StackExchange.Redis StringSet???7882ms
CSRedisCore Set???6101ms
-------------------
StackExchange.Redis StringGet???7729ms
CSRedisCore Get???5762ms
-------------------
StackExchange.Redis StringSetAsync???8094ms
CSRedisCore SetAsync???6315ms
-------------------
StackExchange.Redis StringGetAsync???7986ms
CSRedisClient GetAsync???4931ms
CSRedisCore GetAsync???5960ms
-------------------
CSRedisCore SetAsync(Task.WaitAll)???559ms
StackExchange.Redis StringSetAsync (concurrent Task.WaitAll)???172ms
-------------------
CSRedisCore GetAsync(Task.WaitAll)???435ms
StackExchange.Redis StringGetAsync (concurrent Task.WaitAll)???176ms
```

# Thank

Original open source project: https://github.com/ctstone/csredis

