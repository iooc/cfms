using Cfms.Basic.Interfaces.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cfms.IndentityServer.EntityFramworkCore
{
    [Table("Tenant")]
    public class Tenant : ICreationEntity<int>, IDeletionEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TenantName { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public int DeleteUserId { get; set; }
        public DateTime DeleteTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
