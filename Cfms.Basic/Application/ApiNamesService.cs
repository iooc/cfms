using Cfms.Basic.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Application
{
    /// <summary>
    /// 领域服务名缓存服务
    /// </summary>
    internal class ApiNamesService
    {
        /// <summary>
        /// 服务名类型键值对集合
        /// </summary>
        internal Dictionary<string, Type> AppServices { get; set; }
        internal ApiNamesService()
        {
            AppServices = new Dictionary<string, Type>();
            var ass = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in ass)
            {
                var types = a.GetTypes();
                foreach (var type in types)
                {
                    if (typeof(IAppService).IsAssignableFrom(type) &&
                        !type.IsAbstract && !type.IsInterface)
                    {
                        if (type.Name.EndsWith("AppService"))
                            AppServices.Add(type.Name.Replace("AppService", "").ToLowerInvariant(), type);
                        else
                            AppServices.Add(type.Name.ToLowerInvariant(), type);
                    }
                }
            }
        }
    }
}
