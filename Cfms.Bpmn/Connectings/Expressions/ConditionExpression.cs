using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.BPMN.Connectings.Expressions
{
    /// <summary>
    /// 排他网关，复合网关链接顺序流的条件表达式
    /// </summary>
    public class ConditionExpression
    {
        /// <summary>
        /// 表达式类型(包含表达式和脚本)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 脚本表达式语言名称(默认为JavaScript)
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 表达式内容(现有条件的对线路选择的计算逻辑)
        /// </summary>
        public string Content { get; set; }
    }
}
