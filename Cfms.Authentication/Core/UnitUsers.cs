using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cfms.Authentication.Core
{
    /// <summary>
    /// 部门用户关联表
    /// </summary>
    [Table("Sys_UnitUser")]
    public class UnitUser
    {
        [Key]
        public Guid UnitId { get; set; }
        [Key]
        public Guid UserId { get; set; }
    }
}
