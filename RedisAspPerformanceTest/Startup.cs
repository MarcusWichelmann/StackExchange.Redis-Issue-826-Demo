using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace RedisAspPerformanceTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var client = ConnectionMultiplexer.Connect(new ConfigurationOptions {
                EndPoints = { { "10.200.1.100", 6379 } },
                AbortOnConnectFail = false,
                ConnectTimeout = 15000
            }, Console.Out);

            client.PreserveAsyncOrder = false;

            services.AddSingleton(client);
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
