using Freelancer_s_Web.Models;
using Freelancer_s_Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly FreelancerContext _dbContext;

        public UserRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }

        public async Task DeleteUser(int id)
        {
            var user = await GetUser(id);
            if (user == null)
            {
                throw new Exception("Delete user: User not found");
            }
            user.IsDeleted = false;
            await UpdateUser(user);
        }

        public async Task<User> GetCurrentUser()
        {
            return await GetUser(CustomAuthorization.loginUser.Id);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _dbContext.Users.AsNoTracking().Include(u => u.Major).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task UpdateUser(User user)
        {
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = (await GetCurrentUser()).Email;
            //System.Diagnostics.Debug.WriteLine("From user update repo " + user.CreatedAt.ToString());
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
