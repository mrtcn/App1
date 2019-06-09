using System;
using System.Threading.Tasks;
using App1.Identity.ViewModels;

namespace App1.Identity.Services.Interfaces
{
    public interface ILoyaltySyncService
    {
        Task SyncLoyaltyTable(CreateOrUpdateLoyaltyViewModel model, string accessToken);
    }
}
