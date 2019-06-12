using Cfms.Basic.DependencyInjection;
using Cfms.Basic.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cfms.Basic.Interfaces.Domain.Uow
{
    /// <summary>
    /// 工作单元的声明接口
    /// </summary>
    [Injectable(typeof(UnitOfWork))]
    public interface IUnitOfWork
    {
        DbContext CurrentDbContext { set; }
        /// <summary>
        /// 提交数据更改
        /// </summary>
        /// <returns></returns>
        Task SaveChanges();
    }
}
