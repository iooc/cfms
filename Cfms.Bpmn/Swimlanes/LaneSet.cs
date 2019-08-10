using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Swimlanes
{
    /// <summary>
    /// 泳池，一般用于描述单位
    /// </summary>
    public class LaneSet : List<Lane>, IBpmn
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Task AppendLoad(XElement item, Process target)
        {
            return Task.Run(() =>
            {
                // 加载池道子集
                Executor.LoadSubLaneSet(this, item, target);
            });
        }
    }
}
