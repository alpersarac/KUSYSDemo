using KUSYSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYSDemo.DataAccess.Repository.IRepository
{
    public interface IStudentRepository:IRepository<Student>,IApiCall<Student>
    {
        void Update(Student obj);
    }
}
