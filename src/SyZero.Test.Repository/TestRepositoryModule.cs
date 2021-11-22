using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using SyZero.Domain.Repository;
using SyZero.EntityFrameworkCore;
using SyZero.EntityFrameworkCore.Repositories;

namespace SyZero.Test.Repository
{
    public class TestRepositoryModule : Module
    {
       public TestRepositoryModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            // 首先注册 options，供 DbContext 服务初始化使用
          builder.AddSyZeroEntityFramework<TestDbContext>();
            //注册仓储泛型
          builder.RegisterGeneric(typeof(TestRepositoryBase<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope().PropertiesAutowired();
            ////注册持久化
          builder.RegisterType<UnitOfWork<TestDbContext>>().As<IUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
        }
    }
}



