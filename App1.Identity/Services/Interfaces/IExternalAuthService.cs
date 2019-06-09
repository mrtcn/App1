using System.Threading.Tasks;

namespace App1.Identity.Services.Interfaces
{
    public interface IExternalAuthService<T>
    {
        Task<T> GetAccountAsync(string accessToken);
    }
}
