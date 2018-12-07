using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Dto
{
    /// <summary>
    /// 定义模型必须带租户信息
    /// </summary>
    public interface IMustTenantDto
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        string TenantName { get; set; }
    }
}
