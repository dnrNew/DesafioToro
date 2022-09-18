using DesafioToro.Application.Dtos;
using DesafioToro.Application.Helpers;
using DesafioToro.Domain.User;

namespace DesafioToro.Application.Services
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IUserRepository _userRepository;

        public UserApplicationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUser(int userId)
        {
            var user = await _userRepository.GetUser(userId);
            var userDto = UserHelper.ToDto(user);

            return userDto;
        }
    }
}
