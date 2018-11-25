using Cfms.Basic.Interfaces;
using Cfms.Basic.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Application.Services
{
    public abstract class ApplicationService : IAppService
    {
        ICfmsSession CfmsSession { get; set; }
    }
}
