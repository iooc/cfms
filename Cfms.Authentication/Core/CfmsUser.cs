using Cfms.Basic.Interfaces.Dto;
using Cfms.Basic.Interfaces.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cfms.Authentication.Core
{
    [Table("Sys_User")]
    public class CfmsUser : IdentityUser<long>, IEnity<long>
    {
    }
}
