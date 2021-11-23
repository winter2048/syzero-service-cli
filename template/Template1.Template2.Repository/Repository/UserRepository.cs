using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SyZero.EntityFrameworkCore.Repositories;
using Template1.Template2.Core.Authorization.Users;

namespace Template1.Template2.Repository
{
    public class UserRepository : Template2RepositoryBase<User>, IUserRepository
    {
        public UserRepository(Template2DbContext dbContextProvider):base(dbContextProvider)
        {

        }

        public async Task<string> GetTest()
        {
            return "xxxxxx";

        }


    }
}

