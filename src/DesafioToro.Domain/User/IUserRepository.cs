namespace DesafioToro.Domain.User
{
    public interface IUserRepository : IDisposable
    {
        Task<User> GetUser(int userId);
    }
}
