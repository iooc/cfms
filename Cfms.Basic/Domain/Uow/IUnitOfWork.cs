using Cfms.Basic.DependencyInjection;
using System.Threading.Tasks;

namespace Cfms.Basic.Interfaces.Domain.Uow
{
    public interface IUnitOfWork: ITransientDependency
    {
        Task SaveChanges();
    }
}
