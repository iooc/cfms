using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cfms.BPMN.Extention.ExpressionExtension
{
    /// <summary>
    /// 代表一个完整表达式的对象（非脚本模式）
    /// </summary>
    public class Expression
    {
        /// <summary>
        /// 根据给定的表达式字符串初始化表达式对象
        /// </summary>
        /// <param name="content">表达式字符串</param>
        public Expression(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
                content = content.Replace(" ", "");
            OriginExpression = content;
            ResolvedExpression = content;

            var isbool = Regex.Matches(content, "==").Count;
            if (isbool > 1 || isbool == 0)
                throw new Exception("表达式不合法：表达式有且仅有一个双等号！");

            // 默认已注册变量表达式对象
            RegistryElements<Variable>();

            Resolve().Wait();
        }
        /// <summary>
        /// 解析原始表达式（OriginExpression），生成 ResolvedExpression，
        /// 调用此函数之前请先调用 RegistryElements<T> 注册表达式对象
        /// </summary>
        async System.Threading.Tasks.Task Resolve()
        {
            var elems = OriginExpression.Split(new[] { "==", "}.", "+", "-", "*", "/", "^", "(", ")", " " }, StringSplitOptions.RemoveEmptyEntries);
            var elements = new List<ExpressionElement>();
            await System.Threading.Tasks.Task.Run(() =>
            {
                foreach (var elem in elems)
                {
                    //elem = elem + "}";
                    foreach (var expelement in ElementCollection)
                    {
                        var arr = elem.Split('{');
                        if (expelement.Type == arr[0])
                        {
                            var e = expelement.Clone() as ExpressionElement;
                            e.Original = arr[1];
                            e.DoProcess();
                            if (e.Original != e.Value.ToString()
                                && e.Value != null)
                            {
                                var reg = new Regex($"{e.Type}{{{e.Original}}}");
                                ResolvedExpression = reg.Replace(ResolvedExpression, e.Value.ToString(), 1);
                            }

                            elements.Add(e);
                        }
                    }
                }
                // 检查表达式是否计算完成
                if (ResolvedExpression.IndexOf("{") < 0)
                {
                    // 生成表达式计算结果
                    var expression = ResolvedExpression.Replace("==", "=");

                    DataTable table = new DataTable();
                    var value = table.Compute(expression, "");
                    if ((int)value == 1)
                        Result = true;
                }
            });
        }
        /// <summary>
        /// 获取原始表达式的值
        /// </summary>
        public string OriginExpression { get; private set; }
        /// <summary>
        /// 获取已解析的表达式的字符串形式
        /// </summary>
        public string ResolvedExpression { get; private set; }
        /// <summary>
        /// 已注册全局表达式元素对象的集合
        /// </summary>
        static List<ExpressionElement> ElementCollection { get; set; }
        /// <summary>
        /// 为表达式对象全局注册一个表达式元素处理类(要是扩展元素解析对象生效，必须为类调用此函数)
        /// </summary>
        /// <typeparam name="T">可注册表达式元素的类型</typeparam>
        public static void RegistryElements<T>() where T : ExpressionElement, new()
        {
            if (ElementCollection == null)
                ElementCollection = new List<ExpressionElement>();

            var element = new T();
            if (!ElementCollection.Any(a => a.Type == element.Type))
                ElementCollection.Add(element);
        }
        /// <summary>
        /// 获取表达式计算结果
        /// </summary>
        internal bool Result { get; private set; }
    }
}
