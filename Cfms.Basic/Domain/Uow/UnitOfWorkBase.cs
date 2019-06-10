using Cfms.Basic.Interfaces.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cfms.Basic.Domain.Uow
{
    /// <summary>
    /// 工作单元的基类
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        protected DbContext CurrentDbContext;
        /// <summary>
        /// 提交此工作单元的数据更改
        /// </summary>
        /// <returns></returns>
        public abstract Task SaveChanges();
    }
}
