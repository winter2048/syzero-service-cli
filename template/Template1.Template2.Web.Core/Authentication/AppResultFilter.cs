using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Template1.Template2.Web.Core.Models;

namespace Template1.Template2.Web.Core.Authentication
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class AppResultFilter : IResultFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;


        public void OnResultExecuted(ResultExecutedContext context)
        {
           // context.Result 
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.Result = new JsonResult(new ResultModel((context.Result as ObjectResult)?.Value));
        }
    }



}

