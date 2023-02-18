using KUSYSDemo.DataAccess.Repository.IRepository;
using KUSYSDemo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KUSYSDemo.DataAccess.Repository
{
    public class StudentRepository: Repository<Student>, IStudentRepository
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ILogger<object> _logger;
        public StudentRepository(ApplicationDbContext db, IConfiguration configuration, ILogger<object> logger) :base(db)
        {
            _db = db;
            _configuration = configuration;
            _logger = logger;
        }

        public void Update(Student obj)
        {
            _logger.LogInformation("Student update, " + DateTime.Now);
            _db.Students.Update(obj);
        }

        public async Task<bool> PostAsync(Student obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PostAsJsonAsync("StudentAPI/Create", obj);
                    _logger.LogInformation("Student create new, " + DateTime.Now);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Student create exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
            
        }

        public async Task<Student> GetAsync(int? id)
        {
            Student Student = null;
            if (id == null)
            {
                return Student;
            }

            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                var result = await client.GetAsync($"StudentAPI/GetStudent/{id}");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                    Student = JSserializer.Deserialize<Student>(data);
                    _logger.LogInformation("Student get by id, " + DateTime.Now);
                }
                
            }

            if (Student == null)
            {
                return Student;
            }
            return Student;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            IEnumerable<Student> Students = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var result = await client.GetAsync("StudentAPI/GetAll");

                    if (result.IsSuccessStatusCode)
                    {
                        var data = await result.Content.ReadAsStringAsync();
                        JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                        //deserialize to your class
                        Students = JSserializer.Deserialize<List<Student>>(data);
                        _logger.LogInformation("Student get all, "+DateTime.Now);
                        return Students;
                    }
                    else
                    {
                        Students = Enumerable.Empty<Student>();
                        return Students;
                        //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Student create exception, " + DateTime.Now +": " + ex.Message);
                return null;
            }
            
           
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.DeleteAsync($"StudentAPI/Delete/{id}");
                    _logger.LogInformation("Student delete, " + DateTime.Now);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Student create exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Student obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PutAsJsonAsync("StudentAPI/Update", obj);
                    _logger.LogInformation("Student update, " + DateTime.Now);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Student create exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }
        public async Task<IEnumerable<Course>> CourseListAsync(int? StudentId)
        {
            IEnumerable<Course> StudentsCourse = null;
            if (StudentId == null)
            {
                return StudentsCourse;
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                var result = await client.GetAsync($"StudentAPI/GetCourseList/{StudentId}");
                _logger.LogInformation("Get Courses for Student, " + DateTime.Now);
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                    StudentsCourse = JSserializer.Deserialize<IEnumerable<Course>>(data);
                }

            }

            if (StudentsCourse == null)
            {
                return StudentsCourse;
            }
            return StudentsCourse;
        }

    }
}
