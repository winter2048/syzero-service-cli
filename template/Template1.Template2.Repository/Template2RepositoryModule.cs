using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using SyZero.Domain.Repository;
using SyZero.EntityFrameworkCore;
using SyZero.EntityFrameworkCore.Repositories;

namespace Template1.Template2.Repository
{
    public class Template2RepositoryModule : Module
    {
       public Template2RepositoryModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            // 首先注册 options，供 DbContext 服务初始化使用
          builder.AddSyZeroEntityFramework<Template2DbContext>();
            //注册仓储泛型
          builder.RegisterGeneric(typeof(Template2RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope().PropertiesAutowired();
            ////注册持久化
          builder.RegisterType<UnitOfWork<Template2DbContext>>().As<IUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
        }
    }
}

