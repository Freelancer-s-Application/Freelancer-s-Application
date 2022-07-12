using Repositories.ApplicationForms;
using Repositories.Comments;
using Repositories.Majors;
using Repositories.Messages;
using Repositories.Notifications;
using Repositories.PostContents;
using Repositories.Posts;
using Repositories.Reports;
using Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelancer_s_Web.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationFormRepository ApplicationFormRepository { get; }
        ICommentRepository CommentRepository { get; }
        IMajorRepository MajorRepository { get; }
        IMessageRepository MessageRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IPostRepository PostRepository { get; }
        IPostContentRepository PostContentRepository { get; }
        IReportRepository ReportRepository { get; }
        IUserRepository UserRepository { get; }
        void Save();
    }
}
