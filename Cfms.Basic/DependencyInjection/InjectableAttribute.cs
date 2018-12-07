using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cfms.Basic.DependencyInjection
{
    /// <summary>
    /// 通过对接口或类附加此特性，自动为特性声明的类型产生依赖注入
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Interface | 
        AttributeTargets.Class, 
        AllowMultiple = false)]
    public class InjectableAttribute : Attribute
    {
        /// <summary>
        /// 用于实现接口的类型
        /// </summary>
        internal protected Type Implement { get; protected set; }
        /// <summary>
        /// 键值对应用配置
        /// </summary>
        internal protected IConfiguration Configuration { protected get; set; }
        /// <summary>
        /// 依赖注入对象生命周期
        /// </summary>
        internal protected ServiceLifetime InstanceLifetime { get; protected set; }

        private string ConfigName { get; set; }
        /// <summary>
        /// 使用指定的基类或接口实现类型初始化依赖注入特性的新实例
        /// </summary>
        /// <param name="type">用于实现当前接口的类型</param>
        public InjectableAttribute(Type type)
        {
            Implement = type;
            InstanceLifetime = ServiceLifetime.Transient;
        }
        /// <summary>
        /// 初始化依赖注入特性的新实例
        /// </summary>
        /// <param name="configName">完全限定的配置节表达式</param>
        public InjectableAttribute(string configName)
        {
            ConfigName = configName;
            InstanceLifetime = ServiceLifetime.Transient;
        }
        /// <summary>
        /// 使用指定的基类或接口实现类型初始化依赖注入特性的新实例
        /// </summary>
        /// <param name="type">用于实现当前接口的类型</param>
        /// <param name="lifetime">接口实现对象生命周期</param>
        public InjectableAttribute(Type type, ServiceLifetime lifetime)
        {
            Implement = type;
            InstanceLifetime = lifetime;
        }
        /// <summary>
        /// 初始化依赖注入特性的新实例
        /// </summary>
        /// <param name="configName">完全限定的配置节表达式</param>
        /// <param name="lifetime">接口实现对象生命周期</param>
        public InjectableAttribute(string configName, ServiceLifetime lifetime)
        {
            ConfigName = configName;
            InstanceLifetime = lifetime;
        }
        /// <summary>
        /// 生成配置节表达式给定的指定程序集中的类型
        /// </summary>
        /// <param name="assembly">指定的程序集</param>
        internal void GenerateType(Assembly assembly)
        {
            if (string.IsNullOrWhiteSpace(ConfigName))
                throw new Exception("未指定任何配置节名称");

            var typeName = Configuration.GetValue<string>(ConfigName);
            if (string.IsNullOrWhiteSpace(typeName))
                throw new Exception("无效的配置节名称");

            Implement = assembly.GetType(typeName);
        }
    }
}
