using App1.ServiceBase.Extensions;
using App1.Spot.EntityParams;
using App1.Spot.Services.Interfaces;
using App1.Spot.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App1.Spot
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SpotController : ControllerBase
    {
        private readonly ISpotService _spotService;
        private readonly IPlayerService _playerService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public SpotController(
            ISpotService spotService,
            IIdentityService identityService,
            IPlayerService playerService,
            IMapper mapper)
        {
            _spotService = spotService;
            _identityService = identityService;
            _playerService = playerService;
            _mapper = mapper;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        [Route("spots")]
        public IActionResult Spots([FromBody]CoordinateModel model)
        {
            var spotEntityParamsList = _spotService.GetSpotList(model);
            var spotListViewModel = _mapper.Map<List<SpotEntityParams>, List<SpotListViewModel>>(spotEntityParamsList);

            return Ok(spotListViewModel);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        [Route("FollowingPlayers")]
        public IActionResult FollowingPlayers([FromBody]int spotId)
        {
            var followingPlayers = _spotService.FollowingPlayers(spotId);

            return Ok(followingPlayers);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        [Route("FollowSpot")]
        public async Task<IActionResult> FollowSpot([FromBody]FollowSpotViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var result = await _playerService.AddPlayerSpot(userId, model.SpotId);

            if(result)
                return Ok(new { IsSuccess = true });

            return BadRequest(new { IsSuccess = false });
        }

        [HttpPost]
        [Authorize]
        [Route("createorupdateplayer")]
        public async Task<IActionResult> CreateOrUpdatePlayer([FromBody]CreateOrUpdatePlayerViewModel model)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().GetAccessTokenFromHeaderString();
            var userId = await _identityService.GetUserId(accessToken);

            var playerEntityParams = _mapper.Map<CreateOrUpdatePlayerViewModel, PlayerEntityParams>(model);

            var player = _playerService.GetUserByUserId(userId);

            if (player != null)
                playerEntityParams.Id = player.Id;

            _playerService.CreateOrUpdate(playerEntityParams);

            var result = _mapper.Map<PlayerEntityParams, CreateOrUpdatePlayerViewModel>(playerEntityParams);

            return Ok(result);
        }
    }
}
