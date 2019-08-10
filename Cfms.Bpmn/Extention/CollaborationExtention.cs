using Cfms.BPMN.Flows.Activities;
using Cfms.BPMN.Flows.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.BPMN.Extention
{
    /// <summary>
    /// 图表结构对象扩展帮助
    /// </summary>
    public static class CollaborationExtention
    {
        /// <summary>
        /// 启动给定事件的流程
        /// </summary>
        /// <param name="graph">流程图</param>
        /// <param name="startEvent">开始事件Id</param>
        public static void Start(this Collaboration graph, string startEvent = null)
        {
            StartEvent @event;
            if (string.IsNullOrWhiteSpace(startEvent))
            {
                foreach (var process in graph)
                {
                    foreach (var bpm in process)
                    {
                        if (bpm is StartEvent)
                        {
                            @event = bpm as StartEvent;
                            goto GETEVENT;
                        }
                    }
                }
            }
            else
            {
                foreach (var process in graph)
                {
                    foreach (var bpm in process)
                    {
                        if (bpm.Id == startEvent)
                        {
                            @event = bpm as StartEvent;
                            goto GETEVENT;
                        }
                    }
                }
            }
            // 未查询到开始事件直接跳出
            return;
            GETEVENT:
            foreach (var outgoing in @event.Outgoing)
            {
                var tagetNode = outgoing.TargetRef;
                Executor.Send(tagetNode);
            }
        }
        /// <summary>
        /// 发送一个当前未在内存中的流程到指定任务
        /// </summary>
        /// <param name="graph">已初始化到内存的图表</param>
        /// <param name="task">任务标识</param>
        public static void Send(this Collaboration graph, string task)
        {
            Task taskInstance;
            foreach (var process in graph)
            {
                foreach (var bpm in process)
                {
                    if (bpm.Id == task)
                    {
                        taskInstance = bpm as Task;

                        Executor.Send(taskInstance);
                        return;
                    }
                }
            }
        }
    }
}
