using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SyZero.EntityFrameworkCore;
using SyZero.Domain.Entities;
using SyZero.Test.Core.Authorization.Users;

namespace SyZero.Test.Repository
{
    public class TestDbContext : SyZeroDbContext<TestDbContext>
    {
        public TestDbContext()
        {
           
        }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
           
        }
        
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.LogTo(Console.WriteLine);
        }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
        }

      
    }
}



