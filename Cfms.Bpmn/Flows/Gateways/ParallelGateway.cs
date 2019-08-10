namespace Cfms.BPMN.Flows.Gateways
{
    /// <summary>
    /// 多线同时流出或多线同时流入的并行网关
    /// </summary>
    public class ParallelGateway : Gateway
    {
        internal override void End()
        {
            foreach (var outer in Outgoing)
            {
                // 每条线路都要发送
                EndGateway(outer);
            }
        }
    }
}
