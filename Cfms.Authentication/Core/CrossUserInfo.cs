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
    public class CrossUserInfo : IdentityUser<long>, IEnity<long>
    {
    }
}
