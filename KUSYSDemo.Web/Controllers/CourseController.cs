
using KUSYSDemo.DataAccess.Repository.IRepository;
using KUSYSDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KUSYSDemo.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {

       
        IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var objCourses = await _unitOfWork.Course.GetAllAsync();
            if (objCourses == null)
            {
                TempData["error"] = "An error occured while getting the Courses, please check API settings";
                return RedirectToAction("Index", "Home");
            }
            var Temp = await _unitOfWork.Student.GetAllAsync();
            SelectList StudentSelectList = new SelectList(Temp, "Id", "Name");
            ViewData["StudentList"] = StudentSelectList;
            return View(objCourses);
        }
        //GET
        public async Task<IActionResult> Create()
        {
            var Temp = await _unitOfWork.Student.GetAllAsync();
            SelectList StudentSelectList = new SelectList(Temp, "Id", "Name");

            ViewData["StudentList"] = StudentSelectList;
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course obj)
        {   
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.Course.PostAsync(obj);
                if (result)
                    TempData["success"] = "Course has been created successfully";
                else
                    TempData["error"] = "An error occured while adding the Course, please check API settings";

                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CourseTypeFromDb = await _unitOfWork.Course.GetAsync(id);

            if (CourseTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CourseTypeFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course obj)
        {
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.Course.UpdateAsync(obj);
                if (result)
                    TempData["success"] = "Student has been updated successfully";
                else
                    TempData["error"] = "An error occured while updating the Student, please check API settings";


                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CourseFromDb = await _unitOfWork.Course.GetAsync(id);

            if (CourseFromDb == null)
            {
                return NotFound();
            }

            return View(CourseFromDb);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CourseFromDb = await _unitOfWork.Course.DeleteAsync(id);

            if (CourseFromDb == null)
            {
                TempData["error"] = "An error occured while updating the Student, please check API settings";
                return NotFound();
            }
            TempData["success"] = "Student has been deleted successfully";
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> UpdateCourseDynamically(int StudentId,int CourseId, string CourseName)
        {
            if (CourseName.Length>32)
            {
                TempData["error"] = "Course Name Length can't be longer than 20 character.";
            }
            else
            {
                var Course = await _unitOfWork.Course.UpdateCourseDynamically(CourseId, StudentId, CourseName);
            }
            
            
            return Json("");
        }
    }
}
