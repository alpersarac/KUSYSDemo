using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYSDemo.DataAccess.Repository.IRepository
{
    public interface IApiCallForCourse
    {
        Task<bool> UpdateCourseDynamically(int? CourseId, int StudentId, string? CourseName);
    }
}
