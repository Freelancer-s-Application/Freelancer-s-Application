using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Comments
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllCommentByPostId(int id);
        void CreateComment(Comment comment);
        Task UpdateComment(Comment comment);
    }
}
