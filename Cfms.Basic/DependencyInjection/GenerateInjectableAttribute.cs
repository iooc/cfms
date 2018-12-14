using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Cfms.Basic.DependencyInjection
{
    /// <summary>
    /// 泛形类依赖注入注解类
    /// </summary>
    public class GenerateInjectableAttribute : InjectableAttribute
    {
        public GenerateInjectableAttribute(Type type) : base(type)
        {
        }

        public GenerateInjectableAttribute(string configName) : base(configName)
        {
        }

        public GenerateInjectableAttribute(Type type, ServiceLifetime lifetime) : base(type, lifetime)
        {
        }

        public GenerateInjectableAttribute(string configName, ServiceLifetime lifetime) : base(configName, lifetime)
        {
        }
    }
}
