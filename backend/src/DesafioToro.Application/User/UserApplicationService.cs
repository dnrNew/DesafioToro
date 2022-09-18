using DesafioToro.Application.Dtos;
using DesafioToro.Application.Helpers;
using DesafioToro.Domain.Users;

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

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var usersDto = users.Select(s => UserHelper.ToDto(s)).ToList();

            return usersDto;
        }
    }
}
