using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Authentication
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// 使用 Cfms 认证授权中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseCfmsAuthentication(this IApplicationBuilder app)
        {
            app.UseCookieAuthentication
        }
    }
}
