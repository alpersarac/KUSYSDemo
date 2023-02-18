using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYSDemo.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IStudentRepository Student { get; }
        ICourseRepository Course{ get; }
        void Save();
    }
}
