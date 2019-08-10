using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using Cfms.BPMN.Extention;
using Cfms.BPMN.Flows.Activities;
using Cfms.BPMN.Flows.Events;
using Cfms.BPMN.Flows.Events.Definitions;
using Cfms.BPMN.Flows.Gateways;
using Cfms.BPMN.Swimlanes;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Xml.Linq;

namespace Cfms.BPMN
{
    /// <summary>
    /// BPMN 流程执行器
    /// </summary>
    public static class Executor
    {
        /// <summary>
        /// 将流程发送到下一步执行
        /// </summary>
        /// <returns></returns>
        public static void Send(FlowNode activity)
        {
            if (activity is Activity task)
            {
                // 任务和子进程处理
                task.Execute();
            }
            else if (activity is Gateway gateway)
            {
                // 网关处理
                gateway.Execute();
            }
            else if (activity is IntermediateCatchEvent catchEvent)
            {
                // 中间捕获事件处理
                catchEvent.Execute();
            }
            else if (activity is IntermediateThrowEvent throwEvent)
            {
                // 中间抛出事件处理
                throwEvent.Execute();
            }
            else if (activity is EndEvent @event)
            {
                // 结束事件处理
                @event.Execute();
            }
        }
        #region 解析 bpmn 文档结构
        /// <summary>
        /// 已反射出的所有类型名称的集合
        /// </summary>
        internal static List<string> TypesAll { get; set; }
        /// <summary>
        /// 从 XML 字符串实例化一个流程对象
        /// </summary>
        /// <param name="xml">一个完整文档结构的 xml 字符串</param>
        /// <returns></returns>
        public async static System.Threading.Tasks.Task<Collaboration> Deserialize(string xml)
        {
            var list = new Collaboration();

            var doc = XDocument.Parse(xml.Replace("bpmn2:", "").Replace("bpmn:", ""));
            // 查询流程集合
            if (doc.Element("definitions").Element("collaboration") != null)
            {
                var collaboration = doc.Element("definitions").Element("collaboration").Elements("participant");
                foreach (var elem in collaboration)
                {
                    var @ref = elem.Attribute("processRef").Value;
                    var p = doc.Element("definitions").Elements("process")
                        .Where(a => a.Attribute("id").Value == @ref).FirstOrDefault();
                    if (p != null)
                    {
                        var nameAttr = elem.Attribute("name");
                        var process = new Process
                        {
                            Id = p.Attribute("id").Value,
                            Name = nameAttr?.Value,
                            ExecuteNote = new Subject<ExecutorEventArgs>(),
                            Type = "Process"
                        };

                        await process.AppendLoad(p, process);

                        list.Add(process);
                    }
                }
            }
            // 不存在池道结构时，只有一个流程
            else
            {
                var p = doc.Element("definitions").Elements("process").FirstOrDefault();
                if (p != null)
                {
                    var nameAttr = p.Attribute("name");
                    var process = new Process
                    {
                        Id = p.Attribute("id").Value,
                        Name = nameAttr?.Value,
                        ExecuteNote = new Subject<ExecutorEventArgs>(),
                        Type = "Process"
                    };
                    // 这里需要重写,一个根池道结构一个流程,可以有多个流程
                    await process.AppendLoad(p, process);

                    list.Add(process);
                }
            }
            // 消息、升级、错误、信号，与流程元素平级
            foreach (var ele in doc.Element("definitions").Elements("message"))
            {
                var nameAttr = ele.Attribute("name");
                var msg = new Message
                {
                    Id = ele.Attribute("id").Value,
                    Name = nameAttr?.Value,
                    Type = "Message"
                };
            }
            foreach (var ele in doc.Element("definitions").Elements("escalation"))
            {
                var nameAttr = ele.Attribute("name");
                var msg = new Escalation
                {
                    Id = ele.Attribute("id").Value,
                    Name = nameAttr?.Value,
                    Type = "Escalation"
                };
            }
            foreach (var ele in doc.Element("definitions").Elements("error"))
            {
                var nameAttr = ele.Attribute("name");
                var msg = new Error
                {
                    Id = ele.Attribute("id").Value,
                    Name = nameAttr?.Value,
                    Type = "Error"
                };
            }
            foreach (var ele in doc.Element("definitions").Elements("signal"))
            {
                var nameAttr = ele.Attribute("name");
                var msg = new Signal
                {
                    Id = ele.Attribute("id").Value,
                    Name = nameAttr?.Value,
                    Type = "Signal"
                };
            }
            return list;
        }
        /// <summary>
        /// 获取流程对象的基本信息
        /// </summary>
        /// <typeparam name="T">流程对象的类型</typeparam>
        /// <param name="process">XML 流程根节点元素</param>
        /// <returns>指定类型的可枚举流程对象列表</returns>
        private static IEnumerable<T> GetBaseInfo<T>(XElement process) where T : ModdleElement
        {
            var typeName = typeof(T).Name;
            typeName = typeName[0].ToString().ToLower() + typeName.Substring(1);

            var elems = process.Elements(typeName);

            if (elems.Any())
            {
                var list = new List<T>();

                foreach (var el in elems)
                {
                    var bpmnElement = default(T);
                    bpmnElement.Id = el.Attribute("id").Value;
                    var nameAttr = el.Attribute("name");
                    bpmnElement.Name = nameAttr?.Value;
                    list.Add(bpmnElement);
                }
            }

            return null;
        }
        /// <summary>
        /// 递归加载池道子集
        /// </summary>
        /// <param name="set">池道根节点</param>
        /// <param name="item">池道根元素</param>
        internal static void LoadSubLaneSet(LaneSet set, XElement item, Process process)
        {
            // 道结构
            var lanes = item.Elements("lane");
            foreach (var laneElem in lanes)
            {
                var lane = new Lane
                {
                    Id = laneElem.Attribute("id").Value,
                    Name = laneElem.Attribute("name")?.Value
                };
                var refElems = laneElem.Elements("flowNodeRef");
                foreach (var elem in refElems)
                {
                    var @ref = process.FirstOrDefault(a => a.Id == elem.Value);
                    if (@ref != null)
                        lane.Add(@ref as FlowNode);
                }
                // 子集池道集合
                var sublaneSet = laneElem.Elements("childLaneSet");
                foreach (var subset in sublaneSet)
                {
                    var childLaneSet = new ChildLaneSet
                    {
                        Id = subset.Attribute("id").Value
                    };
                    LoadSubLaneSet(childLaneSet, subset, process);
                    lane.Add(childLaneSet);
                }
                set.Add(lane);
            }
        }
        /// <summary>
        /// 初始化节点流对象，并计算子集
        /// </summary>
        /// <param name="obj">节点流对象的引用</param>
        /// <param name="elem">当前节点 XML 元素</param>
        internal static void Init(this FlowNode obj, XElement elem, List<string> allTypes)
        {
            foreach (var item in elem.Elements())
            {
                if (item.Name.LocalName.Contains("EventDefinition"))
                {
                    var typeName = item.Name.ToString();
                    typeName = typeName[0].ToString().ToUpper() + typeName.Substring(1);
                    var TypeName = allTypes.Where(a => a.Contains("." + typeName)).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(TypeName))
                    {
                        var ass = Assembly.GetExecutingAssembly();
                        var instance = ass.CreateInstance(TypeName);
                        if (obj is BoundaryEvent boundary)
                        {
                            boundary.EventDefinition = (IBoundaryEventDefinition)instance;
                            boundary.EventDefinition.AppendLoad(item, obj.ProcessRef);
                        }
                        else if (obj is StartEvent start)
                        {
                            start.EventDefinition = (IStartEventDefinition)instance;
                            start.EventDefinition.AppendLoad(item, obj.ProcessRef);
                        }
                        else if (obj is EndEvent end)
                        {
                            end.EventDefinition = (IEndEventDefinition)instance;
                            end.EventDefinition.AppendLoad(item, obj.ProcessRef);
                        }
                        else if (obj is IntermediateCatchEvent @catch)
                        {
                            @catch.EventDefinition = (IIntermediateCatchEventDefinition)instance;
                            @catch.EventDefinition.AppendLoad(item, obj.ProcessRef);
                        }
                        else if (obj is IntermediateThrowEvent @throw)
                        {
                            @throw.EventDefinition = (IIntermediateThrowEventDefinition)instance;
                            @throw.EventDefinition.AppendLoad(item, obj.ProcessRef);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
