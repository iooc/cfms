using Cfms.Basic.Interfaces.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cfms.Basic.EntityFrameworkCore
{
    /// <summary>
    /// Cfms 框架数据访问上下文
    /// </summary>
    public class CrossDbContext: DbContext, ICrossDbContext
    {
    }
}
