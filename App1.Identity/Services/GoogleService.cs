using System.Threading.Tasks;
using App1.Identity.Models.GoogleResponse;
using App1.Identity.Services.Interfaces;

namespace App1.Identity.Services
{
    public class GoogleService : IExternalAuthService<GoogleResponse>
    {
        private readonly IExternalAuthClient _googleClient;

        public GoogleService(IExternalAuthClient googleClient)
        {
            _googleClient = googleClient;
        }

        public async Task<GoogleResponse> GetAccountAsync(string accessToken)
        {
            var result = await _googleClient.GetAsync<GoogleResponse>(
                accessToken, "userinfo");

            if (result == null)
            {
                return new GoogleResponse();
            }

            return result;
        }
    }
}
