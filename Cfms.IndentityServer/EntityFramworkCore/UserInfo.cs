using Cfms.Basic.Entity;
using Cfms.Basic.Interfaces.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cfms.IndentityServer.EntityFramworkCore
{
    [Table("UserInfo")]
    public class UserInfo : IdentityUser<Guid>, IMayTenant, IFullAudition<Guid>
    {
        /// <summary>
        /// 用户所属租户
        /// </summary>
        public Guid? TenantId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public int DeleteUserId { get; set; }
        public DateTime DeleteTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
