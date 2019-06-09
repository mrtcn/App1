using System.Security.Claims;
using App1.Identity.Entities;

namespace App1.Identity.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(ApplicationUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
