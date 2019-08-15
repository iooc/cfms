using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.AutoMapper
{
    /// <summary>
    /// 自动映射帮助类
    /// </summary>
    public static class AutoMapperHelper
    {
        /// <summary>
        /// 添加 AutoMapper 自动映射服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCfmsAutoMapper(this IServiceCollection services)
        {
            var ass = AppDomain.CurrentDomain.GetAssemblies();
            //var cfg = new MapperConfigurationExpression();
            //foreach (var a in ass)
            //{
                //var types = a.GetTypes();
                //foreach (var type in types)
                //{
                //    if (type.IsDefined(typeof(AutoMapAttributeBase), true))
                //    {
                //        // 获取此特性类的实例
                //        var attri = type.GetCustomAttributes(typeof(AutoMapAttributeBase), true)[0]
                //            as AutoMapAttributeBase;

                //        attri.CreateMap(cfg, type);
                //    }
                //}
            //}
            //Mapper.Initialize(cfg);
            services.AddAutoMapper(ass);

            return services;
        }
    }
}
