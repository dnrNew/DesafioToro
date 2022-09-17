using DesafioToro.Application.Dtos;
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

    public static class UserHelper
    {
        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Name = user.Name,
                Cpf = user.Cpf,
                Account = user.Account,
                Balance = user.Balance
            };
        }
    }
}
