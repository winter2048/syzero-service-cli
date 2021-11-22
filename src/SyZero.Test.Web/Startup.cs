using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SyZero;
using SyZero.AspNetCore;
using SyZero.AutoMapper;
using SyZero.Log4Net;
using SyZero.Redis;
using SyZero.Web.Common;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using SyZero.Test.Repository;
using SyZero.DynamicWebApi;

namespace SyZero.Test.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfig.Configuration = configuration;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //var nacosOptions = AppConfig.GetSection<NacosServiceOptions>("Nacos");
            //services.AddNacosAspNetCore(serverConf =>
            //{
            //    // serverConf.ServerAddresses = new List<string>() { "http://192.168.10.135:5000" };
            //    serverConf.ServiceName = AppConfig.ServerOptions.Name;
            //    serverConf.Weight = 100;
            //    serverConf.DefaultTimeOut = 15000;
            //    serverConf.LBStrategy = "WeightRandom";
            //    // serverConf.Port = AppConfig.ServerOptions.Port.ToInt32();
            //    // serverConf.Ip = AppConfig.ServerOptions.Ip;
            //}, nacosConf =>
            //{
            //    nacosConf.DefaultTimeOut = 8;
            //    nacosConf.ServerAddresses = nacosOptions.NacosAddresses;
            //    nacosConf.Namespace = "";
            //    nacosConf.ListenInterval = 1000;
            //    nacosConf.UserName = "nacos";
            //    nacosConf.Password = "nacos";
            //    nacosConf.EndPoint = "sub-domain.aliyun.com:8080";
            //});


            // services.AddNacosAspNetCore(Configuration);

            services.AddControllers().AddMvcOptions(options =>
            {
                options.Filters.Add(new SyZero.Test.Web.Core.Authentication.AppVerificationFilter());
                options.Filters.Add(new SyZero.Test.Web.Core.Authentication.AppExceptionFilter());
                options.Filters.Add(new SyZero.Test.Web.Core.Authentication.AppResultFilter());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new LongToStrConverter());
            });

            //动态WebApi
            services.AddDynamicWebApi(new DynamicWebApiOptions()
            {
                DefaultApiPrefix = "/api/Service"
            });
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
           
            #region Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SyZero.Test接口文档",
                    Description = "RESTful API for SyZero.Test",
                    Contact = new OpenApiContact() { Name = "SYZERO", Email = "522112669@qq.com", Url = new Uri("http://test6.syzero.com") }
                });
                options.DocInclusionPredicate((docName, description) => true);
                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                //使用annotation来描述接口  不依赖xml文件
                options.EnableAnnotations();
                // 下面两句，将swagger文档中controller名使用GroupName替换
                // 在Swagger中，一个Tag可以看作是一个API分组
                options.DocInclusionPredicate((_, apiDescription) => string.IsNullOrWhiteSpace(apiDescription.GroupName) == false);
                options.SwaggerGeneratorOptions.TagsSelector = (apiDescription) => new[]
                {
                    apiDescription.GroupName
                };
                var dir = new DirectoryInfo(AppContext.BaseDirectory);
                foreach (FileInfo file in dir.EnumerateFiles("*.xml"))
                {
                    options.IncludeXmlComments(file.FullName);
                }
            });
            #endregion

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //使用AutoMapper
            builder.RegisterModule<AutoMapperModule>();
            //使用SqlSugar仓储
            builder.RegisterModule<TestRepositoryModule>();
            //使用SyZero
            builder.RegisterModule<SyZeroModule>();
            //注入控制器
            builder.RegisterModule<SyZeroControllerModule>();
            //注入Log4Net
            builder.RegisterModule<Log4NetModule>();
            //注入Redis
            builder.RegisterModule<RedisModule>();
            //注入公共层
            builder.RegisterModule<CommonModule>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSyZero();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseStaticFiles(); // For the wwwroot folder

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SyZero.Test.Web API V1");
                c.RoutePrefix = "api/swagger";

            });

            //app.UseConsul();

        }
    }
}



