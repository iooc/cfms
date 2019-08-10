using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.BPMN.Flows.Gateways
{
    /// <summary>
    /// 基于事件的网关，后续链接接收任务或中间捕获事件
    /// 执行第一个发生的中间捕获事件
    /// </summary>
    public class EventBasedGateway : Gateway
    {
        /// <summary>
        /// 暂未实现
        /// </summary>
        /// <returns></returns>
        internal override void End()
        {
            throw new NotImplementedException();
        }
    }
}
