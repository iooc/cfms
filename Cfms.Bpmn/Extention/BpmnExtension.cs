using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using Cfms.BPMN.Flows.Activities;
using Cfms.BPMN.Flows.Events;
using Cfms.BPMN.Flows.Gateways;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace Cfms.BPMN.Extention
{
    public static class BpmnExtension
    {
        /// <summary>
        /// 执行活动任务
        /// </summary>
        /// <param name="task">待执行任务</param>
        internal static void Execute(this IExtensionSupport task)
        {
            if (task.IsAllowToSend)
            {
                // 已完成使命，发送到下一步
                foreach (var @out in task.Outgoing)
                {
                    if (task is FlowNode node)
                        node.ProcessRef.ExecuteNote.OnNext(new ExecutorEventArgs
                        {
                            Type = node.Type,
                            Id = node.Id,
                            Name = node.Name,
                            Message = "已结束！"
                        });

                    Executor.Send(@out.TargetRef);
                    // 任务内存实例还在，需要释放通知服务对象
                    if (task.ChangeNote != null)
                        task.ChangeNote.Dispose();
                }
            }
            else
            {
                // 未完成使命，等待通知
                if (task.ChangeNote == null)
                {
                    task.ChangeNote = new Subject<bool>();

                    task.ChangeNote.Subscribe(observer =>
                    {
                        if (task is FlowNode node)
                            node.ProcessRef.ExecuteNote.OnNext(new ExecutorEventArgs
                            {
                                Type = node.Type,
                                Id = node.Id,
                                Name = node.Name,
                                Message = "运行中！"
                            });
                        task.Execute();
                    });
                }
            }
        }
        /// <summary>
        /// 执行到网关时的处理过程
        /// </summary>
        /// <param name="gateway"></param>
        internal static void Execute(this Gateway gateway)
        {
            // 前置节点,如何判断每条线路都已走过
            gateway.PrevRefCount += 1;
            if (gateway.PrevRefCount >= gateway.Incoming.Count)
            {
                // 开始执行
                gateway.ProcessRef.ExecuteNote.OnNext(new ExecutorEventArgs
                {
                    Type = gateway.Type,
                    Id = gateway.Id,
                    Name = gateway.Name,
                    Message = "运行中！"
                });
                //if (gateway is ExclusiveGateway oneway)
                //{
                //    // 检查下行节点条件
                //    EndExclusiveGateway(oneway);
                //}
                gateway.End();
            }
        }
        /// <summary>
        /// 执行流程结束事件
        /// </summary>
        /// <param name="@event">结束事件对象</param>
        internal static void Execute(this EndEvent @event)
        {
            if (@event.IsAllowToSend)
            {
                // 流程完成
                var process = @event.ProcessRef;
                process.ExecuteNote.OnNext(new ExecutorEventArgs
                {
                    Type = "EndEvent",
                    Id = @event.Id,
                    Name = @event.Name,
                    Message = "进程结束！"
                });
                process.Dispose();
            }
            else
            {
                // 未完成使命，等待通知
                if (@event.ChangeNote == null)
                {
                    @event.ChangeNote = new Subject<bool>();

                    @event.ChangeNote.Subscribe(observer =>
                    {
                        @event.ProcessRef.ExecuteNote.OnNext(new ExecutorEventArgs
                        {
                            Type = "EndEvent",
                            Id = @event.Id,
                            Name = @event.Name,
                            Message = "结束事件运行中！"
                        });
                        @event.Execute();
                    });
                }
            }
        }
    }
}
