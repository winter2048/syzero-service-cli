using System;
using System.Collections.Generic;
using System.Text;
using SyZero.Domain.Entities;
using SyZero.EntityFrameworkCore.Repositories;

namespace Template1.Template2.Repository
{
    public  class Template2RepositoryBase<TEntity> : EfRepository<Template2DbContext,TEntity>
      where TEntity : class, IEntity
    {
        public Template2RepositoryBase(Template2DbContext dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

}

