﻿using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using SyZero;

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
                   builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                   //.AddNacos(cancellationTokenSource.Token) Nacos动态配置
                   .AddConsul(cancellationTokenSource.Token);  //Consul动态配置
               })
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseUrls($"{AppConfig.ServerOptions.Protocol}://*:{AppConfig.ServerOptions.Port}").UseStartup<Startup>();
               }).Build().Run();
        }
    }
}



