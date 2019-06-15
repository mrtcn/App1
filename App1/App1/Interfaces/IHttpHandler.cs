using System;
using System.Threading.Tasks;

namespace App1.Interfaces
{
    public interface IHttpHandler
    {
        Task<T> AuthPostAsync<T>(string token, string baseUrl, string api, string messageBody = null) where T : class;
    }
}
