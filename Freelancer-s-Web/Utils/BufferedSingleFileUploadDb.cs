using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Freelancer_s_Web.Utils
{
    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
