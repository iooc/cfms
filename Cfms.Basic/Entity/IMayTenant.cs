﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Entity
{
    public interface IMayTenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        int? TenantId { get; set; }
    }
}