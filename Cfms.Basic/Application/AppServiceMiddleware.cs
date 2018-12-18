using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cfms.Basic.Application
{
    /// <summary>
    /// 将 Http 请求转发到应用服务的中间件
    /// </summary>
    public class AppServiceMiddleware : IMiddleware
    {
        private ApiNamesService ApiNames;
        private string apiPath;

        IServiceProvider provider;
        internal AppServiceMiddleware(IServiceProvider service)
        {
            provider = service;
            ApiNames = provider.GetService<ApiNamesService>();
            apiPath = "/api/service/";
        }
        internal AppServiceMiddleware(string _apiPath, ApiNamesService _apiNames)
        {
            if (!string.IsNullOrWhiteSpace(_apiPath))
                apiPath = "/api/service/";
            else
                apiPath = _apiPath;

            ApiNames = _apiNames;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.HasValue &&
                context.Request.Path.StartsWithSegments(apiPath, StringComparison.InvariantCultureIgnoreCase))
            {
                var pathArr = context.Request.Path.Value.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                var apiArr = apiPath.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                if (pathArr.Length > apiArr.Length)
                {
                    var className = pathArr[apiArr.Length].ToLowerInvariant();
                    if (ApiNames.AppServices.ContainsKey(className))
                    {
                        var type = ApiNames.AppServices[className];
                        var appservice = provider.GetService(type);
                    }
                }
            }
            //Console.WriteLine(context.Request.Path);
            await next(context);
        }
    }
}
