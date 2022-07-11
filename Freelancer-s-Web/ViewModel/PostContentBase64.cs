using Freelancer_s_Web.Models;

namespace Freelancer_s_Web.ViewModel
{
    public class PostContentBase64
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int PostId { get; set; }
        public string FileBase64 { get; set; }
        public Post Post { get; set; }
    }
}
