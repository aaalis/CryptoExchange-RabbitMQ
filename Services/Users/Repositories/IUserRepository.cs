using Users.Model;

namespace Users.Repositories
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);
        public Task CreateUsers(IEnumerable<User> users);
        public Task<User> GetUserById(int id);
        public Task<User> GetUserByLogin(string login);
        public Task<IEnumerable<User>> GetUsersById(IEnumerable<int> ids);
        public Task<User> DeleteUser(int id);
        public Task DeleteUsers(IEnumerable<int> ids);
        public Task<User> UpdateUser(int id, User user);
        public Task<User> UpdateUserName(int id, string name);
    }
}