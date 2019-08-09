using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Connectings
{
    /// <summary>
    /// 结合关系
    /// </summary>
    public class Association : Connection
    {
        /// <summary>
        /// 暂未实现
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Task ConditionLoad(XElement item)
        {
            throw new NotImplementedException();
        }
    }
}
