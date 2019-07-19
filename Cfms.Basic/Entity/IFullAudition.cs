using Cfms.Basic.Interfaces.Entity;

namespace Cfms.Basic.Entity
{
    /// <summary>
    /// 带完整审计功能的接口声明
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    public interface IFullAudition<Key> : ICreationEntity<Key>, IDeletionEntity<Key>, IUpdateEntity<Key>, ISoftDelete
        where Key:struct
    {
    }
}
