using Users.Model;
using Users.Model.Dto;

namespace Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbcontext;

        public UserRepository(UserDbContext dbContext) 
        {
            _dbcontext = dbContext;
        }

        public async Task<User> CreateUser(User user)
        {
            User newUser = _dbcontext.Users.Add(user).Entity;

            await _dbcontext.SaveChangesAsync();

            return newUser;
        }
    }
}