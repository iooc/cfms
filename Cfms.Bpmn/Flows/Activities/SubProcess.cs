using Cfms.BPMN.Basic.Interfaces;
using Cfms.BPMN.Connectings;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Activities
{
    /// <summary>
    /// 子流程
    /// </summary>
    public class SubProcess : List<IBpmn>, IActivity, IProcess
    {
        public List<SequenceFlow> Incoming { get; set; }
        public List<SequenceFlow> Outgoing { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        /// <summary>
        /// 作为事件子流程时为 true
        /// </summary>
        public bool? TriggeredByEvent { get; set; }
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
        /// <summary>
        /// 获取当前所属父进程对象
        /// </summary>
        public IProcess ProcessRef { get; internal set; }
        public Subject<ExecutorEventArgs> ExecuteNote { get; set; }
        /// <summary>
        /// 暂未实现
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task AppendLoad(XElement item, Process target)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 未实现
        /// </summary>
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
