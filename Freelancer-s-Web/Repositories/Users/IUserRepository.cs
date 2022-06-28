using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(int id);
        int CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<User> GetCurrentUser();
    }
}
