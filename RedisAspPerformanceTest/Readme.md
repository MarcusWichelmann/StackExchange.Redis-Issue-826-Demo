## About

Demo-Project for reproducing a performance issue with StackExchange.Redis when using it together with ASP.Net Core 2.0
More information in the bug report: https://github.com/StackExchange/StackExchange.Redis/issues/826

*Test with:*
`wrk -t8 -c400 -d60s --latency http://localhost:PORT/api/test/method`
using "wrk" from https://github.com/wg/wrk

## Sample-Run

wrk-output:
```
$ wrk -t8 -c400 -d60s --latency http://localhost:55669/api/test/method
Running 1m test @ http://localhost:55669/api/test/method
  8 threads and 400 connections
^C  Thread Stats   Avg      Stdev     Max   +/- Stdev
    Latency     0.00us    0.00us   0.00us    -nan%
    Req/Sec     0.00      0.00     0.00      -nan%
  Latency Distribution
     50%    0.00us
     75%    0.00us
     90%    0.00us
     99%    0.00us
  0 requests in 1.42s, 0.00B read
Requests/sec:      0.00
Transfer/sec:       0.00B
```

Results in this server output:
```
10.200.1.100:6379,connectTimeout=15000,password=*****,abortConnect=False

Connecting 10.200.1.100:6379/Interactive...
BeginConnect: 10.200.1.100:6379
1 unique nodes specified
EndConnect: 10.200.1.100:6379
Connected Interactive/10.200.1.100:6379
Server handshake
Setting client name: MARCUS-PC
Auto-configure...
Sending critical tracer: Interactive/10.200.1.100:6379
Writing to Interactive/10.200.1.100:6379: ECHO
Flushing outbound buffer
Starting read
Requesting tie-break from 10.200.1.100:6379 > __Booksleeve_TieBreak...
Allowing endpoints 00:00:15 to respond...
Response from Interactive/10.200.1.100:6379 / ECHO: BulkString: 16 bytes
Writing to Interactive/10.200.1.100:6379: GET __Booksleeve_TieBreak
Writing to Interactive/10.200.1.100:6379: PING
Response from Interactive/10.200.1.100:6379 / GET __Booksleeve_TieBreak: (null)
Response from Interactive/10.200.1.100:6379 / PING: SimpleString: PONG
10.200.1.100:6379 returned with success
All tasks are already complete
10.200.1.100:6379 had no tiebreaker set
Single master detected: 10.200.1.100:6379
10.200.1.100:6379: Standalone v4.0.8, master; 16 databases; keep-alive: 00:01:00; int: ConnectedEstablished; sub: ConnectedEstablished, 1 active
10.200.1.100:6379: int ops=12, qu=0, qs=0, qc=1, wr=0, sync=10, socks=1; sub ops=3, qu=0, qs=0, qc=0, wr=0, subs=1, sync=3, socks=1
Circular op-count snapshot; int: 0+12=12 (1,20 ops/s; spans 10s); sub: 0+3=3 (0,30 ops/s; spans 10s)
Sync timeouts: 0; fire and forget: 0; last heartbeat: -1s ago
Starting heartbeat...
Hosting environment: Development
Content root path: D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest
Now listening on: http://localhost:55669
Application started. Press Ctrl+C to shut down.
-- StringSet
-- StringSet
-- StringSet
-- StringSet
-- StringSet
-- StringSet
-- StringSet
-- StringSet
-- StringSet
-- StringSet
-- StringSet
-- StringSet
-- StringSet
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15MO", Request id "0HLE4O9VJ15MO:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:d6c59939-414c-4c08-95a9-f01f85a65439, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 4332 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15MK", Request id "0HLE4O9VJ15MK:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:9d51b91b-f0f8-42a8-a91f-961d84bacdf6, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 5098 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15NH", Request id "0HLE4O9VJ15NH:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:3ba8b683-34f5-41f7-9bfe-1088f7b5574d, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 6268 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15NP", Request id "0HLE4O9VJ15NP:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:16537309-2094-4aa1-b92b-762ace8f821e, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 7764 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15NE", Request id "0HLE4O9VJ15NE:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:bf23b542-0348-4604-86f1-729b44a924fc, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 4943 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15ML", Request id "0HLE4O9VJ15ML:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:75c5c2b0-e991-453a-b192-bfef1aaa78f7, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 6179 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15MI", Request id "0HLE4O9VJ15MI:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:30fb49b7-0021-45be-8e38-61182a1f23ac, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 6012 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15O1", Request id "0HLE4O9VJ15O1:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:4295bf22-f056-493d-b54b-14962216d612, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 3215 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15MH", Request id "0HLE4O9VJ15MH:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:ac53dc2a-272d-4359-b597-e11467b62dbe, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 5535 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15NF", Request id "0HLE4O9VJ15NF:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:95502464-8a50-4282-8296-fa9aae7b90c0, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 9939 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15MP", Request id "0HLE4O9VJ15MP:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:682d1213-c071-4bfa-87a4-a2af062603a1, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 971 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15N9", Request id "0HLE4O9VJ15N9:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:ab30e0c0-b834-46fb-a88d-3b5d660632e3, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 13789 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLE4O9VJ15MG", Request id "0HLE4O9VJ15MG:00000001": An unhandled exception was thrown by the application.
StackExchange.Redis.RedisTimeoutException: Timeout performing SET testkey:f58206ab-e865-4c1c-9841-2245a9079bdf, inst: 13, queue: 13, qu: 0, qs: 13, qc: 0, wr: 0, wq: 0, in: 50, ar: 0, clientName: MARCUS-PC, serverEndpoint: 10.200.1.100:6379, keyHashSlot: 13974 (Please take a look at this article for some common client-side issues that can cause timeouts: http://stackexchange.github.io/StackExchange.Redis/Timeouts)
   at StackExchange.Redis.ConnectionMultiplexer.ExecuteSyncImpl[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\ConnectionMultiplexer.cs:line 2120
   at StackExchange.Redis.RedisBase.ExecuteSync[T](Message message, ResultProcessor`1 processor, ServerEndPoint server) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisBase.cs:line 81
   at StackExchange.Redis.RedisDatabase.StringSet(RedisKey key, RedisValue value, Nullable`1 expiry, When when, CommandFlags flags) in c:\code\StackExchange.Redis\StackExchange.Redis\StackExchange\Redis\RedisDatabase.cs:line 1765
   at RedisAspPerformanceTest.Controllers.TestController.Get() in D:\VisualStudio-Projekte\RedisAspPerformanceTest\RedisAspPerformanceTest\Controllers\TestController.cs:line 31
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__10.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeInnerFilterAsync>d__14.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeFilterPipelineAsync>d__17.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.<InvokeAsync>d__15.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.Frame`1.<ProcessRequestsAsync>d__2.MoveNext()
```