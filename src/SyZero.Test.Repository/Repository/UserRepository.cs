using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SyZero.EntityFrameworkCore.Repositories;
using SyZero.Test.Core.Authorization.Users;

namespace SyZero.Test.Repository
{
    public class UserRepository : TestRepositoryBase<User>, IUserRepository
    {
        public UserRepository(TestDbContext dbContextProvider):base(dbContextProvider)
        {

        }

        public async Task<string> GetTest()
        {
            return "xxxxxx";

        }


    }
}



