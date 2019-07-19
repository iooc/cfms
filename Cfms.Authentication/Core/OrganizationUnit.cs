using Cfms.Basic.Entity;
using Cfms.Basic.Interfaces.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cfms.Authentication.Core
{
    public class OrganizationUnit : IEnity<Guid>, IMayTenant, ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 所属租户
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 上级部门
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
