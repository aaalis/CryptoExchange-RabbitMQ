using Users.Model;
using Users.Repositories;
using Users.Model.Dto;
using System.Linq;

namespace Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> CreateUser(User user)
        {
            User newUser = await _userRepository.CreateUser(user);
            return ConvertUser(newUser);
        }

        public async Task CreateUsers(IEnumerable<User> users)
        {
            await _userRepository.CreateUsers(users);
        }

        public async Task<UserDto> GetUserByLogin(string login)
        {
            User user = await _userRepository.GetUserByLogin(login);
            return ConvertUser(user);
        }

        public async Task<UserDto> GetUserById(int id) 
        {
            User user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return null;
            }
            return ConvertUser(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsersById(IEnumerable<int> ids)
        {
            IEnumerable<User> users = await _userRepository.GetUsersById(ids);
            var usersDto = new List<UserDto>(); 
            users.ToList().ForEach(x => usersDto.Add(ConvertUser(x)));
            return usersDto;
        }

        public async Task<User> GetFullFieldsUser(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            User user = await _userRepository.DeleteUser(id);
            return ConvertUser(user);
        }

        public async Task DeleteUsers(IEnumerable<int> ids)
        {
            await _userRepository.DeleteUsers(ids);
        }

        public async Task<UserDto> UpdateUser(int id, User user)
        {
            User updatedUser = await _userRepository.UpdateUser(id, user);
            if (updatedUser == null)
            {
                return null;
            }
            return ConvertUser(updatedUser);
        }

        public async Task<UserDto> UpdateUserName(int id, string newName)
        {
            User user = await _userRepository.UpdateUserName(id, newName);
            return ConvertUser(user);
        }

        private UserDto ConvertUser(User user)
        {
            return new UserDto(user.Login, user.Name);
        }

        // public async Task<IEnumerable<UserDto>> GetUsers(IEnumerable<int> ids)
        // {
        //     _userRepository.;
        // }
    }
}