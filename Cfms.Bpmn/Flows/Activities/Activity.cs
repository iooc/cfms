using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

namespace Cfms.BPMN.Flows.Activities
{
    /// <summary>
    /// 活动对象的基类
    /// </summary>
    public abstract class Activity : IntermediateFlow, IActivity
    {
        /// <summary>
        /// 扩展计算对象的集合
        /// </summary>
        public List<IExtensionObject<IActivity>> ExtensionCollection { get; set; }
        /// <summary>
        /// 是否可以将流程发送到下一步
        /// </summary>
        public bool IsAllowToSend
        {
            get
            {
                if (ExtensionCollection != null && ExtensionCollection.Count > 0)
                    return ExtensionCollection.All(a => a.ComputeResult);
                return true;
            }
        }

        public Subject<bool> ChangeNote { get; set; }
    }
}
