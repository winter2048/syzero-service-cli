using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SyZero.Dependency;
using SyZero.Domain.Repository;
using Template1.Template2.Core.Authorization.Users;

namespace Template1.Template2.Repository
{
    public interface IUserRepository : IRepository<User>
    {
       Task<string> GetTest();
    }
}

