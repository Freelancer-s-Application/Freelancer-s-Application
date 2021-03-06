using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.PostContents
{
    public interface IPostContentRepository : IRepository<PostContent>
    {
        Task<IEnumerable<PostContent>> GetAllPostContentByPostId(int id);    
    }
}
