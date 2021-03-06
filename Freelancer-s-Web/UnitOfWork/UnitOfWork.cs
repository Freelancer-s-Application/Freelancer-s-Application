using Freelancer_s_Web.Models;
using Repositories.ApplicationForms;
using Repositories.Comments;
using Repositories.Majors;
using Repositories.Messages;
using Repositories.PostContents;
using Repositories.Posts;
using Repositories.Reports;
using Repositories.Users;
using System;

namespace Freelancer_s_Web.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        IApplicationFormRepository _applicationFormRepository { get; }
        ICommentRepository _commentRepository { get; }
        IMajorRepository _majorRepository { get; }
        IMessageRepository _messageRepository { get; }
        IPostRepository _postRepository { get; }
        IPostContentRepository _postContentRepository { get; }
        IReportRepository _reportRepository { get; }
        IUserRepository _userRepository { get; }

        private FreelancerContext _db;

        public UnitOfWork(FreelancerContext db)
        {
            _db = db;

            _applicationFormRepository = new ApplicationFormRepository(db);
            _commentRepository = new CommentRepository(db);
            _majorRepository = new MajorRepository(db);
            _messageRepository = new MessageRepository(db);
            _postRepository = new PostRepository(db);
            _postContentRepository = new PostContentRepository(db);
            _reportRepository = new ReportRepository(db);
            _userRepository = new UserRepository(db);
        }

        public IApplicationFormRepository ApplicationFormRepository => _applicationFormRepository;
        public ICommentRepository CommentRepository => _commentRepository;
        public IMajorRepository MajorRepository => _majorRepository;
        public IMessageRepository MessageRepository => _messageRepository;
        public IPostRepository PostRepository => _postRepository;
        public IPostContentRepository PostContentRepository => _postContentRepository;
        public IReportRepository ReportRepository => _reportRepository;
        public IUserRepository UserRepository => _userRepository;

        public void Dispose()
        {
            Console.WriteLine("Unit of work has been dispose");
            _db.Dispose();
        }

        public void Save()
        {
            Console.WriteLine("Unit of work has been saved");
            _db.SaveChanges();
        }
    }
}
