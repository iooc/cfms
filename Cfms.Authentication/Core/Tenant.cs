using Cfms.Basic.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cfms.Authentication.Core
{
    public abstract class Tenant : IEnity<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }
    }
}
