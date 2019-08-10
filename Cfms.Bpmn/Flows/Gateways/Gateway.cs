using Cfms.BPMN.Connectings;

namespace Cfms.BPMN.Flows.Gateways
{
    /// <summary>
    /// 网关对象的基类
    /// </summary>
    public abstract class Gateway : IntermediateFlow
    {
        public Gateway()
        {
            PrevRefCount = 0;
        }
        /// <summary>
        /// 在重写函数中调用 EndGateway() 函数以完成网关的执行，并发送到下行步骤
        /// </summary>
        /// <returns></returns>
        internal abstract void End();
        /// <summary>
        /// 结束此网关，并发送到下一步
        /// </summary>
        /// <param name="conn">网关将要由此连线发送到下一步</param>
        /// <returns></returns>
        protected void EndGateway(Connection conn)
        {
            ProcessRef.ExecuteNote.OnNext(new ExecutorEventArgs
            {
                Type = Type,
                Id = Id,
                Name = Name,
                Message = "已结束！"
            });
            Executor.Send(conn.TargetRef);
        }
        /// <summary>
        /// 是否允许下发
        /// </summary>
        public bool IsAllowToSend { get; internal set; }
        /// <summary>
        /// 已运行过的前置节点的引用计数，当引用计数大于等于前置节点数时，同意下发
        /// </summary>
        /// <remarks>从休眠中唤醒该流程时注意需要根据实际情况手动赋值</remarks>
        internal int PrevRefCount { get; set; }
    }
}
