using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Entity
{
    public interface IMustTenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        Guid TenantId { get; set; }
    }
}
