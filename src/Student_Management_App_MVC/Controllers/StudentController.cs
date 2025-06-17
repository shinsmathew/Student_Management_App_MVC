using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management_App_MVC.Services.Interfaces;

namespace Student_Management_App_MVC.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        public async Task<IActionResult> StudentView()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }
    }
}
