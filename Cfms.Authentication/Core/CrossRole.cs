using Cfms.Basic.Interfaces.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cfms.Authentication.Core
{
    [Table("Sys_Role")]
    public class CrossRole : IdentityRole<Guid>, IEnity<Guid>, IMayTenant
    {
        public Guid? TenantId { get; set; }
    }
}
