using Cfms.Basic.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cfms.Basic.Domain.Uow
{
    /// <summary>
    /// 工作单元服务的默认实现
    /// </summary>
    public class UnitOfWork : UnitOfWorkBase
    {
        /// <summary>
        /// 初始化工作单元服务的新实例
        /// </summary>
        /// <param name="_dbContext">注入每次请求周期的数据访问上下文</param>
        public UnitOfWork(DbContext _dbContext)
        {
            CurrentDbContext = _dbContext;
        }
        /// <summary>
        /// 确认工作单元提交
        /// </summary>
        /// <returns></returns>
        public override Task SaveChanges()
        {
            return CurrentDbContext.SaveChangesAsync();
        }
    }
}
