using Newtonsoft.Json;
using App1.ServiceBase.Enums;

namespace App1.ServiceBase.Interfaces
{
    public interface IHasCulturedEntityStatus
    {
        [JsonIgnore]
        Status CulturedEntityStatus { get; set; }
    }
}
