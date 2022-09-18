using DesafioToro.Application.Dtos;
using DesafioToro.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioToro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserApplicationService _userAppService;

        public UserController(IUserApplicationService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet(Name = "GetUser")]
        public async Task<UserDto> Get(int userId)
        {
            var user = await _userAppService.GetUser(userId);

            return user;
        }
    }
}