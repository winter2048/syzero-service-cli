using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Template1.Template2.Core.Users;

namespace Template1.Template2.Repository
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

