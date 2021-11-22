using System;
using System.Collections.Generic;
using System.Text;
using SyZero.Domain.Entities;
using SyZero.EntityFrameworkCore.Repositories;

namespace SyZero.Test.Repository
{
    public  class TestRepositoryBase<TEntity> : EfRepository<TestDbContext,TEntity>
      where TEntity : class, IEntity
    {
        public TestRepositoryBase(TestDbContext dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

}



