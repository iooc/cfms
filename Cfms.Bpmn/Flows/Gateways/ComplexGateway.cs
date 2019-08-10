using Cfms.BPMN.Connectings;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Gateways
{
    /// <summary>
    /// 复合网关，多线流入和流出
    /// </summary>
    public class ComplexGateway : Gateway
    {
        /// <summary>
        /// 复合网关默认下行连线，当其他条件都不满足时走此线路
        /// </summary>
        public SequenceFlow Default { get; set; }

        public override Task AppendLoad(XElement item, Process target)
        {
            // 还需要为默认连线赋值
            var @in = item.Attribute("default");
            if (@in != null)
                Default = target.Where(a => a.Id == @in.Value).FirstOrDefault() as SequenceFlow;

            return base.AppendLoad(item, target);
        }
        /// <summary>
        /// 复合网关结束函数
        /// </summary>
        /// <returns></returns>
        internal override void End()
        {
            throw new NotImplementedException();
        }
    }
}
