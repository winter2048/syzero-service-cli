using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using syzero.test.Core.Users;

namespace syzero.test.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext dbContextProvider):base(dbContextProvider)
        {

        }

        public async Task<string> GetTest()
        {
            return "xxxxxx";

        }


    }
}

