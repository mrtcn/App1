using Newtonsoft.Json;
using System.Threading.Tasks;
using App1.Identity.GlobalSettings;
using App1.Identity.Services.Interfaces;
using App1.Identity.ViewModels;
using App1.ServiceBase.Handler;

namespace App1.Identity.Services.Sync
{
    public class SpotPlayerSyncService : ISpotPlayerSyncService
    {
        private readonly IHttpHandler _httpHandler;
        public SpotPlayerSyncService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task SyncSpotPlayerTableAsync(CreateOrUpdateSpotPlayerViewModel model, string accessToken)
        {
            var baseUrl = AppSettingsProvider.SpotBaseUrl;

            var api = AppSettingsProvider.CreateOrUpdatePlayer;

            var messageBody = JsonConvert.SerializeObject(model);
            var result = await _httpHandler.AuthPostAsync<CreateOrUpdateSpotPlayerViewModel>(accessToken, baseUrl, api, messageBody);
        }
    }
}
