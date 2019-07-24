using Cfms.Basic.Interfaces.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cfms.IndentityServer.EntityFramworkCore
{
    [Table("RoleInfo")]
    public class RoleInfo : IdentityRole<Guid>, IMayTenant
    {
        /// <summary>
        /// 租户可包含自定义角色信息
        /// </summary>
        public Guid? TenantId { get; set; }
    }
}
