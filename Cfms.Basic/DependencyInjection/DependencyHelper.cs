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
        /// 调用此函数启用 Cfms 特性风格自动依赖注入
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
                        // 获取此特性类的实例
                        var attri = type.GetCustomAttributes(typeof(InjectableAttribute), false)[0];
                        var inject = attri as InjectableAttribute;

                        var serviceType = type;
                        var implementationType = inject.Implement;
                        // 接口声明最后
                        // 实现类其次
                        if (inject.Implement.IsAssignableFrom(type))
                        {
                            serviceType = inject.Implement;
                            implementationType = type;
                        }
                        // 配置模式优先
                        if (inject.Implement == null)
                        {
                            inject.Configuration = configuration;
                            inject.GenerateType(a);

                            implementationType = inject.Implement;
                        }

                        switch (inject.InstanceLifetime)
                        {
                            case ServiceLifetime.Scoped:
                                services.AddScoped(serviceType, implementationType);
                                break;
                            case ServiceLifetime.Singleton:
                                services.AddSingleton(serviceType, implementationType);
                                break;
                            case ServiceLifetime.Transient:
                            default:
                                services.AddTransient(serviceType, implementationType);
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
