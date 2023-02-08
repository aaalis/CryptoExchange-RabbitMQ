using Users.Model;
using Users.Repositories;
using Users.Model.Dto;

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

            return new UserDto(newUser.Login, newUser.Name);
        }
    }
}