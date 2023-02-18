using KUSYSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYSDemo.DataAccess.Repository.IRepository
{
    public interface ICourseRepository: IRepository<Course>, IApiCall<Course>,IApiCallForCourse
    {
        void Update(Course obj);
        IEnumerable<Course> GetCourseListById(int? StudentId);
    }
}
