using Cfms.Basic.Interfaces.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cfms.Basic.EntityFrameworkCore
{
    /// <summary>
    /// Cfms 框架数据访问上下文
    /// </summary>
    public class CrossDbContext: DbContext, ICrossDbContext
    {
        /// <summary>
        /// 使用给定的数据访问上下文可选参数初始化数据访问的基类
        /// </summary>
        /// <param name="options">可选参数</param>
        public CrossDbContext(DbContextOptions options) : base(options) { }
    }
}
