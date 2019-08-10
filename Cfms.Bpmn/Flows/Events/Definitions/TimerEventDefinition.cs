using Cfms.BPMN.Basic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events.Definitions
{
    /// <summary>
    /// 定时器事件定义子元素
    /// </summary>
    public class TimerEventDefinition :
        IStartEventDefinition,
        IIntermediateCatchEventDefinition,
        IBoundaryEventDefinition
    {
        /// <summary>
        /// 设置循环事件周期，形如（R4/2011-03-11T12:13/PT5M）
        /// </summary>
        public string TimeCycle { get; set; }
        /// <summary>
        /// 设置流按指定时间执行
        /// </summary>
        public DateTime TimeDate { get; set; }
        /// <summary>
        /// 设置间隔多久时间后执行此流，形如（P10D，间隔十天）
        /// </summary>
        public string TimeDuration { get; set; }
        /// <summary>
        /// 扩展计算对象的集合
        /// </summary>
        public List<IExtensionObject<IEvent>> ExtensionCollection { get; set; }
        /// <summary>
        /// 加载时间定义（分别是三个类：日期，时间段，循环周期）
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Task AppendLoad(XElement ele, IProcess target)
        {
            throw new NotImplementedException();
        }
    }
}
