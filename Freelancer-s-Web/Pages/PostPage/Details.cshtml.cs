using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.Utils;
using System.IO;
using Freelancer_s_Web.ViewModel;
using Freelancer_s_Web.Commons;

namespace Freelancer_s_Web.Pages.PostPage
{
    [Authorized("USER,ADMIN")]
    public class DetailsModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public DetailsModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [BindProperty]
        public Post Post { get; set; }
        public IList<PostContentBase64> postContents { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }

        public Boolean isAuthor { get; set; }
        public User User { get; set; }

        [BindProperty]
        public Comment comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                postContents = new List<PostContentBase64>();
                using (var work = _unitOfWorkFactory.Get)
                {
                    Post = await work.PostRepository.GetPost(id);
                    if(Post == null)
                    {
                        return NotFound();
                    }
                    if(Post.IsDeleted && CustomAuthorization.loginUser.Role != CommonEnums.ROLE.ADMINISTRATOR)
                    {
                        return NotFound();
                    }

                    var postContentsDb = (await work.PostContentRepository.GetAllPostContentByPostId(id)).ToList();
                    foreach (var content in postContentsDb)
                    {
                        postContents.Add(new PostContentBase64()
                        {
                            Id = content.Id,
                            PostId = content.PostId,
                            Post = content.Post,
                            Type = content.Type,
                            FileBase64 = Convert.ToBase64String(content.File),
                        });
                    }
                    comments = await work.CommentRepository.GetAllCommentByPostId(id);
                }

                if (Post == null)
                {
                    return NotFound();
                }
                if (Post.UserId == CustomAuthorization.loginUser.Id || CustomAuthorization.loginUser.Role == CommonEnums.ROLE.ADMINISTRATOR)
                {
                    isAuthor = true;
                }
                else
                {
                    isAuthor = false;
                }
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return Redirect("/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(int postId)
        {
            try
            {
                postContents = new List<PostContentBase64>();
                using (var work = _unitOfWorkFactory.Get)
                {
                    Post = await work.PostRepository.GetPost(postId);
                    if (Post.UserId == CustomAuthorization.loginUser.Id || CustomAuthorization.loginUser.Role == CommonEnums.ROLE.ADMINISTRATOR)
                    {
                        isAuthor = true;
                    }
                    else
                    {
                        isAuthor = false;
                    }
                    if (Post == null)
                    {
                        return NotFound();

                    }
                    using (var memoryStream = new MemoryStream())
                    {
                        await FileUpload.FormFile.CopyToAsync(memoryStream);

                        // Upload the file if less than 4 MB
                        if (memoryStream.Length < 4194304)
                        {
                            PostContent content = new PostContent()
                            {
                                PostId = postId,
                                Type = FileUpload.FormFile.ContentType,
                                File = memoryStream.ToArray(),
                                CreatedAt = DateTime.Now,
                                CreatedBy = CustomAuthorization.loginUser.Email,
                            };
                            work.PostContentRepository.Add(content);
                            work.Save();
                            Post = await work.PostRepository.GetPost(postId);
                            var postContentsDb = (await work.PostContentRepository.GetAllPostContentByPostId(postId)).ToList();
                            foreach (var pc in postContentsDb)
                            {
                                postContents.Add(new PostContentBase64()
                                {
                                    Id = pc.Id,
                                    PostId = pc.PostId,
                                    Post = pc.Post,
                                    Type = pc.Type,
                                    FileBase64 = Convert.ToBase64String(pc.File),
                                });
                            }
                            comments = await work.CommentRepository.GetAllCommentByPostId(postId);
                            return Page();
                        }
                        else
                        {
                            postContents = new List<PostContentBase64>();
                            Post = await work.PostRepository.GetPost(postId);
                            var postContentsDb = (await work.PostContentRepository.GetAllPostContentByPostId(postId)).ToList();
                            foreach (var pc in postContentsDb)
                            {
                                postContents.Add(new PostContentBase64()
                                {
                                    Id = pc.Id,
                                    PostId = pc.PostId,
                                    Post = pc.Post,
                                    Type = pc.Type,
                                    FileBase64 = Convert.ToBase64String(pc.File),
                                });
                            }
                            comments = await work.CommentRepository.GetAllCommentByPostId(postId);
                            ModelState.AddModelError("File", "The file is too large.");
                            return Page();
                        }
                    }
                }
            } catch(Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return Redirect("/Index");
            }
            
        }
        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            comment = new Comment();
            using (var work = _unitOfWorkFactory.Get)
            {
                try
                {
                    PostContent content = work.PostContentRepository.GetFirstOrDefault(u => u.Id == id, "Post");
                    if (content == null)
                    {
                        return NotFound();
                    }
                    if (content.Post.UserId != CustomAuthorization.loginUser.Id && CustomAuthorization.loginUser.Role != CommonEnums.ROLE.ADMINISTRATOR)
                    {
                        return Redirect("/Authentication/Unauthorized");
                    }
                    content.IsDeleted = true;
                    content.UpdatedAt = DateTime.Now;
                    content.UpdatedBy = CustomAuthorization.loginUser.Email;
                    work.PostContentRepository.UpdatePostContent(content);
                    work.Save();
                    // return Redirect("/PostPage/Details?id=" + content.PostId);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                    return Redirect("/Index");
                }
            }
            try
            {
                postContents = new List<PostContentBase64>();
                using (var work = _unitOfWorkFactory.Get)
                {
                    Post = await work.PostRepository.GetPost(id);

                    var postContentsDb = (await work.PostContentRepository.GetAllPostContentByPostId(id)).ToList();
                    foreach (var content in postContentsDb)
                    {
                        postContents.Add(new PostContentBase64()
                        {
                            Id = content.Id,
                            PostId = content.PostId,
                            Post = content.Post,
                            Type = content.Type,
                            FileBase64 = Convert.ToBase64String(content.File),
                        });
                    }
                    comments = await work.CommentRepository.GetAllCommentByPostId(id);
                }

                if (Post == null)
                {
                    return NotFound();
                }
                if (Post.UserId == CustomAuthorization.loginUser.Id || CustomAuthorization.loginUser.Role == CommonEnums.ROLE.ADMINISTRATOR)
                {
                    isAuthor = true;
                }
                else
                {
                    isAuthor = false;
                }
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                return Redirect("/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //try
            //{
            //comment = new Comment();
                comment.PostId = Post.Id;
                comment.UserId = CustomAuthorization.loginUser.Id;
                comment.CreatedAt = DateTime.Now;
                comment.CreatedBy = CustomAuthorization.loginUser.Email;
                using (var work = _unitOfWorkFactory.Get)
                {
                    await work.CommentRepository.CreateComment(comment);
                }
                return Redirect("/PostPage/Details?id=" + Post.Id);
                //return Page();
            //}
            //catch (Exception ex)
            //{
            //    ViewData["Error"] = ex.Message;
            //    return Page();
            //}
        }

    }
}
