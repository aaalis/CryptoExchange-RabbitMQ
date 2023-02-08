using Users.Model;

namespace Users.Repositories
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);
    }
}