using KUSYSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYSDemo.DataAccess.Repository.IRepository
{
    public interface IApiCall<T> where T : class
    {
        Task<bool> PostAsync(T obj);
        Task<T> GetAsync(int? id);
        Task<bool> UpdateAsync(T obj);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> DeleteAsync(int? id);
        Task<IEnumerable<Course>> CourseListAsync(int? StudentId);
    }
}
