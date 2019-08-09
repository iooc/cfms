using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Bpmn
{
    /// <summary>
    /// 流程设计图中运行定义节点的基类
    /// </summary>
    public class Process : List<IBpmn>, IProcess
    {
        /// <summary>
        /// 流程是否可自动执行
        /// </summary>
        //[XmlAttribute]
        public bool IsExecutable { get; set; }
        //[XmlAttribute]
        public string Id { get; set; }
        //[XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// 进程运行状态通知
        /// </summary>
        public Subject<ExecutorEventArgs> ExecuteNote { get; set; }
        public string Type { get; set; }
        /// <summary>
        /// 加载 XML 流程子节点到流程类
        /// </summary>
        /// <param name="process"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Task AppendLoad(XElement process, Process target)
        {
            var list = process.Elements();

            var ass = Assembly.GetExecutingAssembly();
            var types = ass.DefinedTypes.Select(a => a.FullName).ToList();
            Executor.TypesAll = types;

            var subject = new Subject<bool>();
            var tasks = new List<Task>();

            foreach (var item in list)
            {
                var typeName = item.Name.ToString();
                typeName = typeName[0].ToString().ToUpper() + typeName.Substring(1);
                var TypeName = types.Where(a => a.Contains("." + typeName)).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(TypeName))
                {
                    var obj = ass.CreateInstance(TypeName);
                    var @base = (IBpmn)obj;
                    @base.Id = item.Attribute("id").Value;
                    var nameAttr = item.Attribute("name");
                    @base.Name = nameAttr?.Value;
                    @base.Type = typeName;
                    Add(@base);
                    // 等待循环完成后,处理流对象之间的引用
                    subject.Subscribe(res =>
                    {
                        var task = @base.AppendLoad(item, this);
                        tasks.Add(task);
                    });
                }
            }
            // 处理流对象之间的引用
            subject.OnNext(true);
            subject.OnCompleted();
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 对进程对象进行终极清理
        /// </summary>
        public void Dispose()
        {
            foreach (var bpmn in this)
            {
                if (bpmn is IExtensionSupport ie)
                {
                    if (ie.ChangeNote != null && !ie.ChangeNote.IsDisposed)
                    {
                        ie.ChangeNote.Dispose();
                        ie.ChangeNote = null;
                    }
                }
            }
            this.Clear();
            // 释放状态通知对象
            if (ExecuteNote != null && !ExecuteNote.IsDisposed)
            {
                ExecuteNote.Dispose();
                ExecuteNote = null;
            }
        }
    }
    /// <summary>
    /// 执行状态通知事件数据
    /// </summary>
    public class ExecutorEventArgs
    {
        /// <summary>
        /// 节点类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 节点标识
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
    }
}
