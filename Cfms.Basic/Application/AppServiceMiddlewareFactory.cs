using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Application
{
    /// <summary>
    /// AppService 中间件创建工厂
    /// </summary>
    public class AppServiceMiddlewareFactory : IMiddlewareFactory
    {
        public AppServiceMiddlewareFactory(IServiceProvider serviceProvider)
        {
        }

        public IMiddleware Create(Type middlewareType)
        {
            throw new NotImplementedException();
        }

        public void Release(IMiddleware middleware)
        {
            throw new NotImplementedException();
        }
    }
}
