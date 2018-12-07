﻿using System.Threading.Tasks;

namespace Cfms.Basic.Interfaces.Domain.Uow
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
