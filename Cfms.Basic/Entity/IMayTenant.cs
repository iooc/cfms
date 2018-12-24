using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Entity
{
    /// <summary>
    /// 启用可选租户数据
    /// </summary>
    public interface IMayTenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        int? TenantId { get; set; }
    }
}
