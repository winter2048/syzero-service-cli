using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SyZero;

namespace SyZero.Test.Web.Core.Authentication
{
    public class AppVerificationFilter : IActionFilter, IOrderedFilter
    {

        public int Order => 1;

        /// <summary>
        /// 每次请求Action之前发生，，在行为方法执行前执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Thread.CurrentPrincipal = null;
            context.HttpContext.User = null;
            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                string token = context.HttpContext.Request.Headers["Authorization"];

                var Principal = GetPrincipal(token);

                if (Principal != null)
                {
                    Thread.CurrentPrincipal = Principal;
                    context.HttpContext.User = Principal;
                }
            }
          
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// 此方法用解码字符串token，并返回秘钥的信息对象
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler(); // 创建一个JwtSecurityTokenHandler类，用来后续操作
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken; // 将字符串token解码成token对象
                if (jwtToken == null)
                    return null;

                var validationParameters = new TokenValidationParameters() // 生成验证token的参数
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = AppConfig.GetSection("JWT:audience"),//Audience
                    ValidIssuer = AppConfig.GetSection("JWT:issuer"),//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.GetSection("JWT:SecurityKey")))//拿到SecurityKey
                };
                SecurityToken securityToken; // 接受解码后的token对象

                return tokenHandler.ValidateToken(token, validationParameters, out securityToken);
            }

            catch
            {
                return null;
            }
        }

    }
}



