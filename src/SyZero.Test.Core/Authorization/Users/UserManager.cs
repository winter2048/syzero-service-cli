using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SyZero.Domain.Repository;
using SyZero.Domain.Service;

namespace SyZero.Test.Core.Authorization.Users
{
    public class UserManager : DomainService
    {
        private readonly IRepository<User> _userRepository;
        public UserManager(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateAsync(User user)
        {
            await _userRepository.AddAsync(user);
           // await UnitOfWork.SaveAsyncChange();
            return user;
        }
    }
}



