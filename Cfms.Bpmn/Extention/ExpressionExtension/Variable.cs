using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.BPMN.Extention.ExpressionExtension
{
    /// <summary>
    /// 表示表达式内的字符串是一个变量
    /// </summary>
    public sealed class Variable : ExpressionElement
    {
        protected override void Init()
        {
            Type = "$";
        }
        /// <summary>
        /// 不知道这个应该处理什么样的变量(哪儿来的变量)
        /// </summary>
        protected internal override void DoProcess()
        {
            //throw new System.NotImplementedException();
            Value = Original;
        }
    }
}
