using System;
using System.Collections.Generic;
using System.Text;
using SyZero.Domain.Entities;
using SyZero.SqlSugar.Repositories;

namespace Template1.Template2.Repository
{
    public class RepositoryBase<TEntity> : SqlSugarRepository<DbContext,TEntity>
       where TEntity : class, IEntity, new()
    {
        public RepositoryBase(DbContext dbContext) : base(dbContext)
        {

        }
    }

}

