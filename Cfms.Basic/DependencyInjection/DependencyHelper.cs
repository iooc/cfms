using Cfms.Basic.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cfms.Basic.DependencyInjection
{
    /// <summary>
    /// 依赖注入帮助类
    /// </summary>
    public static class DependencyHelper
    {
        /// <summary>
        /// 调用此函数实现 Cfms 特性风格自动依赖注入
        /// </summary>
        public static IServiceCollection AddCfmsAttibuteInjection(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var ass = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in ass)
            {
                var types = a.GetTypes();
                foreach (var type in types)
                {
                    // 注入特性
                    if (type.IsDefined(typeof(InjectableAttribute),false))
                    {
                        var attri = type.GetCustomAttributes(typeof(InjectableAttribute), false)[0];
                        var injectable = attri as InjectableAttribute;
                        if (injectable.Implement == null)
                        {
                            injectable.Configuration = configuration;
                            injectable.GenerateType(a);
                        }

                        switch (injectable.InstanceLifetime)
                        {
                            case ServiceLifetime.Transient:
                                services.AddTransient(type, injectable.Implement);
                                break;
                            case ServiceLifetime.Scoped:
                                services.AddScoped(type, injectable.Implement);
                                break;
                            case ServiceLifetime.Singleton:
                                services.AddSingleton(type, injectable.Implement);
                                break;
                        }
                    }
                    // API 应用服务的依赖注入
                    if (typeof(IAppService).IsAssignableFrom(type) &&
                        !type.IsAbstract && !type.IsInterface)
                    {
                        services.AddTransient(type);
                    }
                }
            }
            return services;
        }
    }
}
