using App1.ServiceBase.ServiceBase.Models;

namespace App1.ServiceBase.Interfaces
{
    public interface IRemoveEntityParams : IHasOperator, IHasCulture, IEntity
    {
        bool CheckRelationalEntities { get; set; }
        bool RemovePermanently { get; set; }
    }
}
