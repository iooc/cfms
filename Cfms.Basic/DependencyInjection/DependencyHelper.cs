using Cfms.Basic.Application;
using Cfms.Basic.Application.Services;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

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
                        if (implementationType.IsAssignableFrom(serviceType)||
                            Array.Exists(serviceType.GetInterfaces(),
                            t => t.IsGenericType && t.GetGenericTypeDefinition() == implementationType))
                        {
                            serviceType = inject.Implement;
                            implementationType = type;

                            var descriptor = services.FirstOrDefault(desc => desc.ServiceType == serviceType);
                            if (descriptor != null)
                                services.RemoveAll(serviceType);
                        }
                        else
                        {
                            // 需要多重依赖覆盖时，此似乎不是最优解决办法，
                            // 只有第一个会生效，不能实现自由选取有效依赖
                            var descriptor = services.FirstOrDefault(desc => desc.ServiceType == serviceType);
                            if (descriptor != null)
                                continue;
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
        /// <summary>
        /// 以中间件的方式添加 Cfms 领域驱动服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCfmService(
            this IServiceCollection services)
        {
            //// 领域服务名缓存服务
            //services.AddSingleton<ApiNamesService>();
            //// 激活中间件工厂
            //services.AddTransient(provider => new AppServiceMiddleware(provider));
            // 控制器功能提供程序模式
            services.AddMvcCore(options =>
            {
                // 使用领域服务路由变换约定
                var routeToken = new RouteTokenTransformerConvention(new AppServiceParameterTransformer());
                options.Conventions.Add(routeToken);
            })
            .ConfigureApplicationPartManager(manager =>
            {
                // 添加领域驱动服务功能提供程序到功能集中
                var provider = new AppServiceControllerFeatureProvider();
                manager.FeatureProviders.Add(provider);
            });

            return services;
        }
    }
}
