using System.Threading.Tasks;
using App1.Identity.ViewModels;

namespace App1.Identity.Services.Interfaces
{
    public interface ISpotPlayerSyncService
    {
        Task SyncSpotPlayerTableAsync(CreateOrUpdateSpotPlayerViewModel model, string accessToken);
    }
}
