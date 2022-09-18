using DesafioToro.Application.Dtos;
using DesafioToro.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioToro.Api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private IUserApplicationService _userAppService;

        public UserController(IUserApplicationService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet("{userId}")]      
        public async Task<UserDto> GetUser(int userId)
        {
            var user = await _userAppService.GetUser(userId);

            return user;
        }

        [HttpGet()]
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userAppService.GetAllUsers();

            return users;
        }
    }
}