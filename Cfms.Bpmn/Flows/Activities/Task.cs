using Cfms.BPMN.Connectings;

namespace Cfms.BPMN.Flows.Activities
{
    /// <summary>
    /// 任务
    /// </summary>
    public class Task : Activity
    {
        /// <summary>
        /// 任务默认下行连线
        /// </summary>
        public Connection @default { get; set; }
    }
}
