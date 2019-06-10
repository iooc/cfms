using Cfms.Basic.Interfaces.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Domain.Uow
{
    public class UnitOfWorkManager
    {
        internal IUnitOfWork Current;

        public UnitOfWorkManager(IUnitOfWork uow)
        {
            Current = uow;
        }
    }
}
