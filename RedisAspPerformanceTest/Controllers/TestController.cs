using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisAspPerformanceTest.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ConnectionMultiplexer _client;

        public TestController(ConnectionMultiplexer client)
        {
            _client = client;
        }

        // Test with:
        // wrk -t8 -c400 -d60s --latency http://localhost:PORT/api/test/method
        // using "wrk" from https://github.com/wg/wrk

        [HttpGet("method")]
        public IActionResult Get()
        {
            IDatabase database = _client.GetDatabase();
            string key = $"testkey:{Guid.NewGuid()}";

            Console.WriteLine("-- StringSet");
            database.StringSet(key, "test");
            Console.WriteLine("-- -- Return");

            return Ok();
        }
    }
}
