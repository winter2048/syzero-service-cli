using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SyZero.Application.Service;
using SyZero.Web.Common;

namespace Template1.Template2.IApplication
{
   public interface IUploadAppService : IApplicationServiceBase
    {

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="sname"></param>
        /// <param name="size"></param>
        /// <param name="ftype"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<FileData> UploadFile(IFormFile file);
    }
}

