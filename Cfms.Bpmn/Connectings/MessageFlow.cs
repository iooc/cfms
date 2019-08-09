using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Connectings
{
    /// <summary>
    /// 消息流
    /// </summary>
    public class MessageFlow : Connection
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
