using Newtonsoft.Json;
using System.Threading.Tasks;
using App1.Identity.GlobalSettings;
using App1.Identity.Services.Interfaces;
using App1.Identity.ViewModels;
using App1.ServiceBase.Handler;

namespace App1.Identity.Services.Sync
{
    public class LoyaltySyncService : ILoyaltySyncService
    {
        private readonly IHttpHandler _httpHandler;
        public LoyaltySyncService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task SyncLoyaltyTable(CreateOrUpdateLoyaltyViewModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.LoyaltyBaseUrl;

            var api = AppSettingsProvider.CreateOrUpdateLoyalty;

            var messageBody = JsonConvert.SerializeObject(model);
            var result = await _httpHandler.AuthPostAsync<CreateOrUpdateLoyaltyViewModel>(accessToken, baseUrl, api, messageBody);
        }
    }
}
