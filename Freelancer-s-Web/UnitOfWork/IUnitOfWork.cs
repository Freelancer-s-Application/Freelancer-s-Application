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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationFormRepository ApplicationFormRepository { get; }
        ICommentRepository CommentRepository { get; }
        IMajorRepository MajorRepository { get; }
        IMessageImageRepository MessageImageRepository { get; }
        IMessageRepository MessageRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IPostRepository PostRepository { get; }
        IPostContentRepository PostContentRepository { get; }
        IReportRepository ReportRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IUserRepository UserRepository { get; }
        void Save();
    }
}
