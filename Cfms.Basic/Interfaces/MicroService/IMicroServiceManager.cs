using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.MicroService
{
    /// <summary>
    /// 不依赖第三方工具自维护微服务声明接口
    /// </summary>
    interface IMicroServiceManager
    {
        /// <summary>
        /// 分布式服务订阅网关服务并注册
        /// </summary>
        void Subscribe();
    }
}
