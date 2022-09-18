using DesafioToro.Application.Dtos;

namespace DesafioToro.Application.Services
{
    public interface IUserApplicationService
    {
        Task<UserDto> GetUser(int userId);
        Task<List<UserAssetDto>> GetUserAssets(int userId);
    }
}
