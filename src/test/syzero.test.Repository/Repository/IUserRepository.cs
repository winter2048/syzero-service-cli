using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SyZero.Dependency;
using SyZero.Domain.Repository;
using syzero.test.Core.Users;

namespace syzero.test.Repository
{
    public interface IUserRepository : IRepository<User>
    {
       Task<string> GetTest();
    }
}

