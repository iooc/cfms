using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cfms.Basic.MicroService
{
    /// <summary>
    /// API 网关中间件
    /// </summary>
    public class ApiGatewayMiddleware : IMiddleware
    {
        /// <summary>
        /// 调用中间件执行过程
        /// </summary>
        /// <param name="context">Http 请求上下文</param>
        /// <param name="next">后续请求调用</param>
        /// <returns></returns>
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
