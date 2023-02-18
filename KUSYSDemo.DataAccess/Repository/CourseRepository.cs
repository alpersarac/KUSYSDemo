using KUSYSDemo.DataAccess.Repository.IRepository;
using KUSYSDemo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using System.Net.Http.Json;

namespace KUSYSDemo.DataAccess.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ILogger<object> _logger;
        public CourseRepository(ApplicationDbContext db, IConfiguration configuration, ILogger<object> logger) :base(db)
        {
            _db = db;
            _configuration = configuration;
            _logger = logger;
        }

        public void Update(Course obj)
        {
            _logger.LogInformation("Course update, " + DateTime.Now);
            _db.Courses.Update(obj);
        }

        public IEnumerable<Course> GetCourseListById(int? id)
        {
            _logger.LogInformation("Course get by id, " + DateTime.Now);
            return _db.Courses.Where(p => p.Student.Id == id).ToList();
        }
        public IEnumerable<Course> GetAll()
        {
            _logger.LogInformation("Course get all, " + DateTime.Now);
            return _db.Courses.ToList();
        }

        public async Task<bool> PostAsync(Course obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PostAsJsonAsync("CourseAPI/Create", obj);
                    _logger.LogInformation("Course create, " + DateTime.Now);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Course create exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        public async Task<Course> GetAsync(int? id)
        {
            Course Course = null;
            if (id == null)
            {
                return Course;
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                var result = await client.GetAsync($"CourseAPI/GetCourse/{id}");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                    Course = JSserializer.Deserialize<Course>(data);
                    _logger.LogInformation("Course get by id, " + DateTime.Now);
                }

            }

            if (Course == null)
            {
                return Course;
            }
            return Course;
        }

        public async Task<bool> UpdateAsync(Course obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PutAsJsonAsync("CourseAPI/Update", obj);
                    _logger.LogInformation("Course update, " + DateTime.Now);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Course update exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            IEnumerable<Course> Courses = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var result = await client.GetAsync("CourseAPI/GetAll");

                    if (result.IsSuccessStatusCode)
                    {
                        var data = await result.Content.ReadAsStringAsync();
                        JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                        //deserialize to your class
                        Courses = JSserializer.Deserialize<List<Course>>(data);
                        _logger.LogInformation("Course get all, " + DateTime.Now);
                        return Courses;
                    }
                    else
                    {
                        Courses = Enumerable.Empty<Course>();
                        return Courses;
                        //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Course get all exception, " + DateTime.Now + ": " + ex.Message);
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

                    var response = await client.DeleteAsync($"CourseAPI/Delete/{id}");
                    _logger.LogInformation("Course delete, " + DateTime.Now);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Course delete exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        public Task<IEnumerable<Course>> CourseListAsync(int? StudentId)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> UpdateCourseDynamically(int? CourseId, int StudentId, string? CourseName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var Items = new { CourseId = CourseId, StudentId = StudentId, CourseName= CourseName };
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);
                    
                    //var response = await client.PostAsJsonAsync($"CourseAPI/UpdateCourseDynamically/{CourseId}/{StudentId}/{CourseName}");
                    var response = await client.PostAsJsonAsync($"CourseAPI/UpdateCourseDynamically/{CourseId}/{StudentId}/{CourseName}", Items);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Course dynamic update exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }
    }
}
