
using KUSYSDemo.DataAccess.Repository.IRepository;
using KUSYSDemo.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUSYSDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        public StudentAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Student> GetAll()
        {
            IEnumerable<Student> objCategories = _unitOfWork.Student.GetAll();
            return objCategories;
        }
        
        [HttpGet("GetStudent/{id}")]
        public Student GetStudent(int? id)
        {
            var StudentFromDb = _unitOfWork.Student.GetFirstOrDefault(u => u.Id == id);
            return StudentFromDb;
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create([FromBody] Student obj)
        {
            _unitOfWork.Student.Add(obj);
            _unitOfWork.Save();
        }

        [HttpPut]
        [Route("Update")]
        public async Task Update([FromBody] Student obj)
        {
            _unitOfWork.Student.Update(obj);
            _unitOfWork.Save();
        }

        [HttpDelete("Delete/{id}")]
        public void Delete(int? id)
        {
            var obj = _unitOfWork.Student.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.Student.Remove(obj);
            _unitOfWork.Save();
        }
        [HttpGet("GetCourseList/{id}")]
        public IEnumerable<Course> GetCourseList(int? id)
        {
            var StudentCourse = _unitOfWork.Course.GetCourseListById(id);
            return StudentCourse;
        }
    }
}
