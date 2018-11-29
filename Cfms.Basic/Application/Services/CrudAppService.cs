using Cfms.Basic.Interfaces.Application;
using Cfms.Basic.Interfaces.Domain;
using Cfms.Basic.Interfaces.Dto;
using Cfms.Basic.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cfms.Basic.Application.Services
{
    public abstract class CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
        : ApplicationService, ICrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : IEnity<TPrimaryKey>
    {
        protected IRepository<TEntity, TPrimaryKey> Repository;

        public CrudAppService(IRepository<TEntity, TPrimaryKey> repository)
        {

        }
        public Task<TEntityDto> Create(TCreateInput input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TDeleteInput input)
        {
            throw new NotImplementedException();
        }

        public Task<TEntityDto> Get(TGetInput input)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResultDto<TEntityDto, TPrimaryKey>> GetAll(TGetAllInput input)
        {
            throw new NotImplementedException();
        }

        public Task<TEntityDto> Update(TUpdateInput input)
        {
            throw new NotImplementedException();
        }
    }
}
