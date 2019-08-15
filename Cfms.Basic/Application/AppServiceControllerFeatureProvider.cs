using AutoMapper.Internal;
using Cfms.Basic.Application.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cfms.Basic.Application
{
    /// <summary>
    /// 领域驱动服务控制器功能提供程序
    /// </summary>
    /// <remarks>
    /// 通过向 AspNetCore 添加功能集的方式实现领域驱动服务
    /// </remarks>
    public class AppServiceControllerFeatureProvider : ControllerFeatureProvider
    {
        /// <summary>
        /// 判断给定类型是否可转换为控制器
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        protected override bool IsController(TypeInfo typeInfo)
        {
            var type = typeInfo.AsType();
            
            if (typeof(IAppService).IsAssignableFrom(type))
            {
                if (!typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType)
                    return false;
                return true;
            }

            return base.IsController(typeInfo);
        }
    }
}
