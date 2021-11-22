﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyZero.Test.Repository
{
    //这个类需要在开发时从命令行运行“dotnet ef…”命令
    public class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        private IConfiguration _configuration;
        public TestDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TestDbContext CreateDbContext(string[] args)
        {
            var configuration = _configuration;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            var connectionString = configuration.GetConnectionString("sqlConnection");
            if (configuration.GetConnectionString("type").ToLower() == "mysql")
                builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)));
            else
                builder.UseSqlServer(connectionString);
            return new TestDbContext(builder.Options);
        }
    }
}



