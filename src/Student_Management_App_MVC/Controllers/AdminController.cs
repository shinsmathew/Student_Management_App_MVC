using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management_App_MVC.Models.DTOs.Student;
using Student_Management_App_MVC.Services.Interfaces;
using Student_Management_App_MVC.Validators;

namespace Student_Management_App_MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IStudentService _studentService;
        public AdminController(ILogger<AdminController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }
        public async Task<IActionResult> AdminView()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }

        [HttpGet]
        public IActionResult AdminAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminAdd(StudentCreateDto studentDto)
        {
            var validator = new StudentCreateValidator();
            var validationResult = await validator.ValidateAsync(studentDto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(studentDto);
            }

            await _studentService.AddStudentAsync(studentDto);
            return RedirectToAction("AdminView");
        }

        [HttpGet]
        public async Task<IActionResult> AdminEdit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentDto = new StudentUpdateDto
            {
                
                StudentName = student.StudentName,
                StudentAddress = student.StudentAddress,
                City = student.City,
                ZipCode = student.ZipCode,
                State = student.State,
                Country = student.Country,
                StudentEmail = student.StudentEmail,
                PhoneNumber1 = student.PhoneNumber1,
                PhoneNumber2 = student.PhoneNumber2,
                DateOfBirth = student.DateOfBirth,
                SchoolName = student.SchoolName,
                Course = student.Course
            };

            return View(studentDto);
        }

        [HttpPost]
        public async Task<IActionResult> AdminEdit(StudentUpdateDto studentDto)
        {
            var validator = new StudentUpdateValidator();
            var validationResult = await validator.ValidateAsync(studentDto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(studentDto);
            }

            await _studentService.UpdateStudentAsync(studentDto);
            return RedirectToAction("AdminView");
        }

        [HttpPost]
        public async Task<IActionResult> AdminDelete(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction("AdminView");
        }
    }
}