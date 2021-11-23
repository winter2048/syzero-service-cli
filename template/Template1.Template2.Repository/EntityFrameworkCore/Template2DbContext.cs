using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SyZero.EntityFrameworkCore;
using SyZero.Domain.Entities;
using Template1.Template2.Core.Authorization.Users;

namespace Template1.Template2.Repository
{
    public class Template2DbContext : SyZeroDbContext<Template2DbContext>
    {
        public Template2DbContext()
        {
           
        }

        public Template2DbContext(DbContextOptions<Template2DbContext> options) : base(options)
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

