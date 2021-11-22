using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SyZero;
using SyZero.Application.Service;
using SyZero.Web.Common;
using SyZero.Test.IApplication;

namespace SyZero.Test.Application
{
    public class UploadAppService : IUploadAppService
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>
        /// result
        /// </returns>
        [HttpPost("UploadFile")]
        public async Task<FileData> UploadFile(IFormFile file)
        {
            return await Task.Run(() =>
            {
                if (file != null)
                {
                    var dto = AliyunOssHelper.UpLoadSingleFile(file, "syzero-blog");
                    if (dto.Status)
                    {
                        return dto.Data;
                    }
                    else
                    {
                        throw new SyMessageBox(dto.Message);
                    }
                }
                throw new SyMessageBox("请选择文件");
            });
        }
    }
}



