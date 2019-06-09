using Newtonsoft.Json;
using App1.ServiceBase.Enums;

namespace App1.ServiceBase.Interfaces
{
    public interface IHasStatus
    {
        [JsonIgnore]
        Status Status { get; set; }
    }
}
