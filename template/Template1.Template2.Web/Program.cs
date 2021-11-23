using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SyZero;
using SyZero.Consul;
using SyZero.Nacos;

namespace Template1.Template2.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((hostingContext, builder) =>
               {
                   builder.AddNacos(cancellationTokenSource.Token)  //Nacos动态配置
                                                                    //.AddConsul(cancellationTokenSource.Token)  //Consul动态配置
                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
               })
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseUrls($"{AppConfig.ServerOptions.Protocol}://*:{AppConfig.ServerOptions.Port}").UseStartup<Startup>();
               }).Build().Run();
        }
    }
}

