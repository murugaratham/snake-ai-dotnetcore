using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace snakeAiDnc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            {
                var path = Directory.GetCurrentDirectory() + "/snake-ai";
                if (args.Length > 0)
                {
                    path = args[0];
                }
                return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseKestrel()
                    .UseContentRoot(path)
                    .UseWebRoot(path)
                    .UseUrls(@"http://0.0.0.0:5000")
                    .Build();
            }
        }
    }
}
