using Freelancer_s_Web.DataAccess;
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
        //User GetUser(int id);
        int CreateUser(User user);
        //void UpdateUser(User user);
        //void DeleteUser(User user);
    }
}
