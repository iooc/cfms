using System.Collections.Generic;

namespace Cfms.BPMN.Basic.Interfaces
{
    /// <summary>
    /// 活动对象定义接口
    /// </summary>
    public interface IActivity : IIntermediateFlow, IExtensionSupport
    {
        List<IExtensionObject<IActivity>> ExtensionCollection { get; set; }
        //bool IsAllowToSend { get; }
    }
}
