using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.BPMN.Basic.Interfaces
{
    /// <summary>
    /// 事件定义接口
    /// </summary>
    public interface IEvent : IExtensionSupport
    {
        ///// <summary>
        ///// 事件计算结果，是否允许下发，默认为 True
        ///// </summary>
        //bool IsAllowToSend { get; }
    }
}
