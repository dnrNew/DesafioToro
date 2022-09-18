using DesafioToro.Domain.UserAssets;

namespace DesafioToro.Domain.Users
{
    public interface IUserRepository : IDisposable
    {
        Task<User> GetUser(int userId);
        Task<List<User>> GetAllUsers();
        Task SaveUserExecutedOrder(User user, int stockId);
    }
}
