using System;
using System.Threading.Tasks;

namespace App1.Interfaces
{
    public interface IHttpHandler
    {
        T AuthPost<T>(string token, string baseUrl, string api, string messageBody = null) where T : class;
        Task<T> AuthPostAsync<T>(string token, string baseUrl, string api, string messageBody = null) where T : class;
    }
}
