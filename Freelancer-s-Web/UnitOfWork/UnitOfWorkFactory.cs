using Freelancer_s_Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWorkFactory
    {
        public UnitOfWork Get { get { return new UnitOfWork(new FreelancerContext()); } }

        public IDisposable UnitOfWork { get; set; }
    }
}