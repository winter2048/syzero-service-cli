using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Template1.Template2.Web.Core.Models;
using SyZero.Domain.Repository;
using SyZero.Cache;
using SyZero;
using SyZero.Runtime.Security;
using System.Threading;

namespace Template1.Template2.Web.Core.Authentication
{


    /// <summary>
    /// Action 拦截验证授权码
    /// </summary>
    public class ApiCheckTokenFilterAttribute : ActionFilterAttribute ,IOrderedFilter
    {
        public new int Order => 2;
        /// <summary>
        /// 每次请求Action之前发生，，在行为方法执行前执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (Thread.CurrentPrincipal == null)
                throw new SyMessageBox(SyMessageBoxStatus.UnAuthorized);

        }


       

    }

 



}

