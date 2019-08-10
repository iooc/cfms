using Cfms.BPMN.Flows.Events.Definitions;
using System.Collections.Generic;

namespace Cfms.BPMN
{
    /// <summary>
    /// 一个完整流程图表结构
    /// </summary>
    public class Collaboration : List<Process>
    {
        /// <summary>
        /// 进程图表超时回收设置，单位为小时（默认为一天24小时）
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// 获取消息对象的集合
        /// </summary>
        List<Message> Messages { get; }
        /// <summary>
        /// 获取升级对象的集合
        /// </summary>
        List<Escalation> Escalations { get; }
        /// <summary>
        /// 获取错误对象的集合
        /// </summary>
        List<Error> Errors { get; }
        /// <summary>
        /// 获取信号对象的集合
        /// </summary>
        List<Signal> Signals { get; set; }
    }
}
