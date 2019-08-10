﻿using Cfms.BPMN.Connectings;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Gateways
{
    /// <summary>
    /// 包容网关，执行所有满足条件多线下发
    /// </summary>
    public class InclusiveGateway : Gateway
    {
        /// <summary>
        /// 包容网关默认下行连线，当其他条件都不满足时走此线路
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
        /// 暂未实现
        /// </summary>
        /// <returns></returns>
        internal override void End()
        {
            if (Outgoing.Count == 1)
            {
                // 单条线路立即执行
                EndGateway(Outgoing[0]);
            }
            else
            {
                var have = false;
                // 多条线路，找出所有满足条件的线路
                foreach (var outer in Outgoing)
                {
                    // 计算表达式
                    var expression = outer.Expression;
                    if (Default != outer && expression != null)
                    {
                        // 走符合条件线路
                        //expression.Content
                        if (expression.Type == "Script")
                        {

                        }
                        else
                        {
                            var express = new Extention.ExpressionExtension.Expression(expression.Content);
                            if (express.Result)
                            {
                                // 听说判断赋值效能更差，能不用判断尽量不用
                                have = true;
                                EndGateway(outer);
                            }
                        }
                    }
                }
                if (!have && Default != null)
                {
                    // 走默认线路
                    EndGateway(Default);
                }
                else
                {
                    ProcessRef.ExecuteNote.OnNext(new ExecutorEventArgs
                    {
                        Type = Type,
                        Id = Id,
                        Name = Name,
                        Message = "错误：未找到任何可运行分支，流程暂停！"
                    });
                }
            }
        }
    }
}
