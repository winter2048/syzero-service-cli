using System;
using System.Collections.Generic;
using System.Text;

namespace Template1.Template2.IApplication.Users.Dto
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int Type { get; set; } = 0;
    }
}
