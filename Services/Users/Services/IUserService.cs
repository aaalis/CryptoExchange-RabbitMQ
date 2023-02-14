using Users.Model;
using Users.Model.Dto;

namespace Users.Services
{
    public interface IUserService
    {
        public Task<UserDto> CreateUser(User user);
        public Task CreateUsers(IEnumerable<User> users);
        public Task<UserDto> GetUserById(int id);
        public Task<UserDto> GetUserByLogin(string login);
        public Task<IEnumerable<UserDto>> GetUsersById(IEnumerable<int> ids);
        public Task<User> GetFullFieldsUser(int id);
        public Task<UserDto> UpdateUser(int id, User user);
        public Task<UserDto> UpdateUserName(int id, string newName);
        public Task<UserDto> DeleteUser(int id);
        public Task DeleteUsers(IEnumerable<int> ids);
    }    
}