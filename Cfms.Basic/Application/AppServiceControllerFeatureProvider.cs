using AutoMapper.Internal;
using Cfms.Basic.Application.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cfms.Basic.Application
{
    public class AppServiceControllerFeatureProvider: ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var type = typeInfo.AsType();

            if (!typeof(IAppService).IsAssignableFrom(type) ||
                !typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType)
            {
                return false;
            }

            //var remoteServiceAttr = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(typeInfo);

            //if (remoteServiceAttr != null && !remoteServiceAttr.IsEnabledFor(type))
            //{
            //    return false;
            //}

            //var configuration = _iocResolver.Resolve<AbpAspNetCoreConfiguration>().ControllerAssemblySettings.GetSettingOrNull(type);
            //return configuration != null && configuration.TypePredicate(type);
            return base.IsController(typeInfo);
        }
    }
}
