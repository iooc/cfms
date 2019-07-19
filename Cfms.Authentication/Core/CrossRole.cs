using Cfms.Basic.Entity;
using Cfms.Basic.Interfaces.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cfms.Authentication.Core
{
    [Table("Sys_Role")]
    public class CrossRole : IdentityRole<Guid>, IEnity<Guid>, IMayTenant, ISoftDelete
    {
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 角色软删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
