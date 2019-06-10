using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic
{
    /// <summary>
    /// IServiceProvider 扩展函数类
    /// </summary>
    public static class IServiceProviderExtensions
    {
        /// <summary>
        /// 获取一个通过类型参数指定类型的服务对象的实例
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <param name="provider">依赖注入服务提供程序</param>
        /// <returns></returns>
        public static T GetService<T>(this IServiceProvider provider)
        {
            var type = provider.GetService(typeof(T));
            if (type is T result)
                return result;
            return default;
        }
    }
}
