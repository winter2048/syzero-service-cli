using System;
using System.Collections.Generic;
using System.Text;
using SyZero.Domain.Entities;

namespace Template1.Template2.Core.Authorization.Users
{
    public class User : Entity
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }


    }
}

