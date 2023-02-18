using KUSYSDemo.DataAccess.Repository.IRepository;
using KUSYSDemo.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUSYSDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        public CourseAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Course> GetAll()
        {
            IEnumerable<Course> objCourses = _unitOfWork.Course.GetAll();
            return objCourses;
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create([FromBody] Course obj)
        {
            _unitOfWork.Course.Add(obj);
            _unitOfWork.Save();
        }
        [HttpGet("GetCourse/{id}")]
        public Course GetCourse(int? id)
        {
            var StudentFromDb = _unitOfWork.Course.GetFirstOrDefault(u => u.Id == id);
            return StudentFromDb;
        }
        [HttpPut]
        [Route("Update")]
        public async Task Update([FromBody] Course obj)
        {
            _unitOfWork.Course.Update(obj);
            _unitOfWork.Save();
        }

        [HttpDelete("Delete/{id}")]
        public void Delete(int? id)
        {
            var obj = _unitOfWork.Course.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.Course.Remove(obj);
            _unitOfWork.Save();
        }

        [HttpPost("UpdateCourseDynamically/{CourseId}/{StudentId}/{CourseName}")]
        public void UpdateCourseDynamically(int? CourseId,int StudentId, string? CourseName)
        {
            var obj = _unitOfWork.Course.GetFirstOrDefault(u => u.Id == CourseId);
            obj.StudentId = StudentId;
            obj.Name = CourseName;
            _unitOfWork.Save();
        }

    }
}
