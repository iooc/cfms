using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Swimlanes
{
    /// <summary>
    /// 泳道结构，一般用于描述部门(不是必要的)
    /// </summary>
    public class Lane : List<ILane>, IBpmn
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Task AppendLoad(XElement item, Process target)
        {
            throw new System.NotImplementedException();
        }
    }
}
