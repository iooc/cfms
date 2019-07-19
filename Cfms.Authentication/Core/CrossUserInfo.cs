using Cfms.Basic.Entity;
using Cfms.Basic.Interfaces.Dto;
using Cfms.Basic.Interfaces.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cfms.Authentication.Core
{
    [Table("Sys_UserInfo")]
    public class CrossUserInfo : IdentityUser<Guid>, IEnity<Guid>, IMayTenant, ISoftDelete
    {
        /// <summary>
        /// 租户 Id
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 该用户是否是已被删除的用户
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
