using Cfms.BPMN.Connectings.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Connectings
{
    /// <summary>
    /// 顺序流
    /// </summary>
    public class SequenceFlow : Connection
    {
        /// <summary>
        /// 作为排他、复合网关等的条件表达式逻辑对象
        /// </summary>
        public ConditionExpression Expression { get; set; }

        protected override Task ConditionLoad(XElement item)
        {
            return Task.Run(() =>
            {
                var sub = item.Element("conditionExpression");
                if (sub != null)
                {
                    Expression = new ConditionExpression();
                    var language = sub.Attribute("language");
                    if (language != null)
                        Expression.Language = language.Value;
                    Expression.Content = sub.Value;
                }
            });
        }
    }
}
