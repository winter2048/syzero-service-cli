using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template1.Template2.Repository
{
    //这个类需要在开发时从命令行运行“dotnet ef…”命令
    public class Template2DbContextFactory : IDesignTimeDbContextFactory<Template2DbContext>
    {
        private IConfiguration _configuration;
        public Template2DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Template2DbContext CreateDbContext(string[] args)
        {
            var configuration = _configuration;
            var builder = new DbContextOptionsBuilder<Template2DbContext>();
            var connectionString = configuration.GetConnectionString("sqlConnection");
            if (configuration.GetConnectionString("type").ToLower() == "mysql")
                builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)));
            else
                builder.UseSqlServer(connectionString);
            return new Template2DbContext(builder.Options);
        }
    }
}

