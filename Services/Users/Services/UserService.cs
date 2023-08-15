using Users.Model;
using Users.Repositories;
using Users.Model.Dto;
using System.Linq;
using Users.Services.Cache;
using Users.Services.Rabbit;

namespace Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClient _rabbitClient;
        private readonly ICacheService _cacheService;

        public UserService(IUserRepository userRepository, IClient rabbitClient, ICacheService cacheService)
        {
            _userRepository = userRepository;
            _rabbitClient = rabbitClient;
            _cacheService = cacheService;
        }

        public async Task<UserDto> CreateUser(User user)
        {
            User newUser = await _userRepository.CreateUser(user);
            if (newUser != null)
            {
                int id = newUser.Id;
                CreatePortfolio(id);
            }
            return ConvertUser(newUser);
        }

        public async Task CreateUsers(IEnumerable<User> users)
        {
            await _userRepository.CreateUsers(users);
        }

        public async Task<UserDto> GetUserByLogin(string login)
        {
            var key = "getOrderByLogin" + login;
            
            var cache = GetCache<UserDto>(key);
            if (cache != null)
            {
                return cache;
            }
            
            UserDto user = ConvertUser(await _userRepository.GetUserByLogin(login));

            AddCache(key, user);
            
            return user;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var key = "getUserById" + id;

            var cache = GetCache<UserDto>(key);
            if (cache != null)
            {
                return cache;
            }
            
            UserDto user = ConvertUser(await _userRepository.GetUserById(id));

            AddCache(key, user);
            
            return user;
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
            var key = "getFullFieldsUser" + id;

            var cache = GetCache<User>(key);
            if (cache != null)
            {
                return cache;
            }
            
            var user = await _userRepository.GetUserById(id);

            AddCache(key, user);

            return user;
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            User user = await _userRepository.DeleteUser(id);
            DeletePortfolio(id);
            _cacheService.RemoveData("getUserById" + id);
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

        private void CreatePortfolio(int id) 
        {
            _rabbitClient.CreatePortfolio(id);
        }

        private void DeletePortfolio(int id) 
        {
            _rabbitClient.DeletePortfolio(id);
        }
        private T? GetCache<T>(string key)
        {
            var cacheData = _cacheService.GetData<T>(key);
            if (cacheData != null)
            {
                return cacheData;
            }

            return default;
        }

        private bool AddCache<T>(string key, T value, int liveTimeInSeconds = 30)
        {
            var expTime = DateTimeOffset.Now.AddSeconds(liveTimeInSeconds);
            return _cacheService.SetData(key, value, expTime);
        }
    }
}
