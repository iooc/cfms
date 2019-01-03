using Cfms.Basic.Interfaces.Entity;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cfms.IndentityServer.EntityFramworkCore
{
    public class DeviceFlow : DeviceFlowCodes, IEnity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
