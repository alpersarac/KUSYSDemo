
using KUSYSDemo.DataAccess.Repository.IRepository;
using KUSYSDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace KUSYSDemo.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        IUnitOfWork _unitOfWork;
        
        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var objCategories  = await _unitOfWork.Student.GetAllAsync();
            if (objCategories==null)
            {
                TempData["error"] = "An error occured while getting the Categories, please check API settings";
                return RedirectToAction("Index","Home");
            }
            return View(objCategories);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student obj)
        {
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.Student.PostAsync(obj);
                if (result)
                    TempData["success"] = "Student has been created successfully";
                else
                    TempData["error"] = "An error occured while adding the Student, please check API settings";

                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Post


        //GET
        public async Task<IActionResult>  Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var StudentFromDb = await _unitOfWork.Student.GetAsync(id);

            if (StudentFromDb == null)
            {
                return NotFound();
            }

            return View(StudentFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student obj)
        {

            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.Student.UpdateAsync(obj);
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

            var StudentFromDb = await _unitOfWork.Student.GetAsync(id);

            if (StudentFromDb == null)
            {
                return NotFound();
            }

            return View(StudentFromDb);
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

            var StudentFromDb = await _unitOfWork.Student.DeleteAsync(id);

            if (StudentFromDb == null)
            {
                TempData["error"] = "An error occured while updating the Student, please check API settings";
                return NotFound();
            }
            TempData["success"] = "Student has been deleted successfully";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> List(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var StudentFromDb = await _unitOfWork.Student.GetAsync(id);

            if (StudentFromDb == null)
            {
                return NotFound();
            }
            ViewBag.CourseList = await _unitOfWork.Student.CourseListAsync(id);

            return View(StudentFromDb);
        }
    }
}
