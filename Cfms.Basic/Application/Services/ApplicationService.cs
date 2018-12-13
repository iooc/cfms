using Cfms.Basic.Interfaces;
using Cfms.Basic.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Application.Services
{
    public abstract class ApplicationService : IAppService
    {
        /// <summary>
        /// 用户登录标识
        /// </summary>
        ICfmsSession CfmsSession { get; set; }
    }
}
