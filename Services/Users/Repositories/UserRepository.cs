using Users.Model;
using Users.Model.Dto;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

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

        public async Task CreateUsers(IEnumerable<User> users)
        {
            await _dbcontext.Users.AddRangeAsync(users);
            await _dbcontext.SaveChangesAsync();
            
        }

        public async Task<User> GetUserById(int id) 
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(x=>x.Id == id && x.IsDeleted == false);
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(x=>x.Login == login && x.IsDeleted == false);
        }

        public async Task<IEnumerable<User>> GetUsersById(IEnumerable<int> ids)
        {
            ids = ids.Distinct();
            return await _dbcontext.Users.Where(x => ids.Contains(x.Id) && x.IsDeleted == false).ToListAsync();
        }

        public async Task<User> DeleteUser(int id)
        {
            User user = await _dbcontext.Users.Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(user != null) 
            {
                user.IsDeleted = true;
                _dbcontext.Update(user);
                await _dbcontext.SaveChangesAsync();
            }
            return user;
        }

        public async Task DeleteUsers(IEnumerable<int> ids)
        {
            ids = ids.Distinct();
            await _dbcontext.Users.Where(x => ids.Contains(x.Id) && x.IsDeleted == false)
                                .ForEachAsync(x => x.IsDeleted = true);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            User updUser = await _dbcontext.Users.FirstOrDefaultAsync(x=>x.Id == id && x.IsDeleted == false);
            if (updUser == null)
            {
                return null;
            }
            updUser.ChangeData(user);
            _dbcontext.Users.Update(updUser);
            await _dbcontext.SaveChangesAsync();
            return updUser;
        }

        public async Task<User> UpdateUserName(int id, string name)
        {
            User user = await _dbcontext.Users.FirstOrDefaultAsync(x=>x.Id == id && x.IsDeleted == false);
            if (user != null)
            {
                user.Name = name;
                _dbcontext.Users.Update(user);
                await _dbcontext.SaveChangesAsync();
            }
            return user;
        }
    }
}