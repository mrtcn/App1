using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace App1.Spot.Hubs
{
    [Authorize]
    public class SpotHubs: Hub
    {
    }
}
