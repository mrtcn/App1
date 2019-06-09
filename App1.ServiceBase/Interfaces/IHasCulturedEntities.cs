using System.Collections.Generic;

namespace App1.ServiceBase.Interfaces
{
    public interface IHasCulturedEntities<TCulturedEntity>
    {
        ICollection<TCulturedEntity> CulturedEntities { get; set; }
    }
}
