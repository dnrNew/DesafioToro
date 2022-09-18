using DesafioToro.Application.Dtos;
using DesafioToro.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioToro.Api.Controllers
{
    [ApiController]
    [Route("user/[action]")]
    public class UserController : ControllerBase
    {
        private IUserApplicationService _userAppService;

        public UserController(IUserApplicationService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet(Name = "GetUser")]
        public async Task<UserDto> GetUser(int userId)
        {
            var user = await _userAppService.GetUser(userId);

            return user;
        }

        [HttpGet(Name = "GetUserAssets")]
        public async Task<List<UserAssetDto>> GetUserAssets(int userId)
        {
            var userAssets = await _userAppService.GetUserAssets(userId);

            return userAssets;
        }
    }
}