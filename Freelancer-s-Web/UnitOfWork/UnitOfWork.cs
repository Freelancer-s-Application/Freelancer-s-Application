using Freelancer_s_Web.Models;
using Repositories.ApplicationForms;
using Repositories.Comments;
using Repositories.Majors;
using Repositories.MessageImages;
using Repositories.Messages;
using Repositories.Notifications;
using Repositories.PostContents;
using Repositories.Posts;
using Repositories.Reports;
using Repositories.Reviews;
using Repositories.Users;
using System;

namespace Freelancer_s_Web.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        IApplicationFormRepository _applicationFormRepository { get; }
        ICommentRepository _commentRepository { get; }
        IMajorRepository _majorRepository { get; }
        IMessageImageRepository _messageImageRepository { get; }
        IMessageRepository _messageRepository { get; }
        INotificationRepository _notificationRepository { get; }
        IPostRepository _postRepository { get; }
        IPostContentRepository _postContentRepository { get; }
        IReportRepository _reportRepository { get; }
        IReviewRepository _reviewRepository { get; }
        IUserRepository _userRepository { get; }

        private FreelancerContext _db;

        public UnitOfWork(FreelancerContext db)
        {
            _db = db;

            _applicationFormRepository = new ApplicationFormRepository(db);
            _commentRepository = new CommentRepository(db);
            _majorRepository = new MajorRepository(db);
            _messageImageRepository = new MessageImageRepository(db);
            _messageRepository = new MessageRepository(db);
            _notificationRepository = new NotificationRepository(db);
            _postRepository = new PostRepository(db);
            _postContentRepository = new PostContentRepository(db);
            _reportRepository = new ReportRepository(db);
            _reviewRepository = new ReviewRepository(db);
            _userRepository = new UserRepository(db);
        }

        public IApplicationFormRepository ApplicationFormRepository => _applicationFormRepository;
        public ICommentRepository CommentRepository => _commentRepository;
        public IMajorRepository MajorRepository => _majorRepository;
        public IMessageImageRepository MessageImageRepository => _messageImageRepository;
        public IMessageRepository MessageRepository => _messageRepository;
        public INotificationRepository NotificationRepository => _notificationRepository;
        public IPostRepository PostRepository => _postRepository;
        public IPostContentRepository PostContentRepository => _postContentRepository;
        public IReportRepository ReportRepository => _reportRepository;
        public IReviewRepository ReviewRepository => _reviewRepository;
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
