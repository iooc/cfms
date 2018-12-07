using Cfms.Basic.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Dto
{
    /// <summary>
    /// 租户数据可有可无
    /// </summary>
    public interface IMayTenantDto: IMayTenant
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        string TenantName { get; set; }
    }
}
