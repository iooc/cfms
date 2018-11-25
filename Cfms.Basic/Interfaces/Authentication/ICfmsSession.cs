using Cfms.Basic.Interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Cfms.Basic.Interfaces.Authentication
{
    /// <summary>
    /// 用户认证标识传递对象
    /// </summary>
    public interface ICfmsSession: IMayTenantDto, IIdentity
    {
        /// <summary>
        /// 用户 Id
        /// </summary>
         long UserId { get; set; }
    }
}
