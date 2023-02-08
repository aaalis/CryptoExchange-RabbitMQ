using Users.Model;
using Users.Model.Dto;

namespace Users.Services
{
    public interface IUserService
    {
        public Task<UserDto> CreateUser(User user);
    }    
}