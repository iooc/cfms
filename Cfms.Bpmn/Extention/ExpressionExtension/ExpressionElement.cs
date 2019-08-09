using System;

namespace Cfms.BPMN.Extention.ExpressionExtension
{
    /// <summary>
    /// 表达式元素对象定义的基类
    /// </summary>
    public abstract class ExpressionElement : ICloneable
    {
        public ExpressionElement()
        {
            Init();
        }
        /// <summary>
        /// 重写此函数，并在函数中为 Type 属性赋值以指定表达式前缀
        /// </summary>
        protected abstract void Init();
        /// <summary>
        /// 重写此函数，以对表达式元素进行处理获取 Value 值
        /// </summary>
        protected internal abstract void DoProcess();
        /// <summary>
        /// 表达式元素本来的值
        /// </summary>
        public string Original { get; set; }
        /// <summary>
        /// 表达式解析值
        /// </summary>
        public object Value { get; protected set; }
        /// <summary>
        /// 创建对象的浅表副本，创建新对象
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// 表达式元素的前缀类型
        /// </summary>
        internal protected string Type { get; set; }
    }
}
