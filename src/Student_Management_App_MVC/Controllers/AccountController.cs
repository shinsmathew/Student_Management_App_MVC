using Microsoft.AspNetCore.Mvc;
using Student_Management_App_MVC.Enums;
using Student_Management_App_MVC.Models.DTOs.User;
using Student_Management_App_MVC.Services.Interfaces;
using Student_Management_App_MVC.Validators;

namespace Student_Management_App_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var Validation = new UserLoginValidator();
            var ValidationResult = await Validation.ValidateAsync(userLoginDto);
            if (!ValidationResult.IsValid)
            {
                foreach (var error in ValidationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(userLoginDto);
            }

            var result = await _userService.LoginUserAsync(userLoginDto);
            if(!result)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(userLoginDto);
            }

            // Get the user to check their role
            var user = await _userService.GetUserByUsernameAsync(userLoginDto.Username);

            // Redirect based on role
            return RedirectToAction(user.Role == UserRole.Admin ? "AdminView" : "StudentView",
                                 user.Role == UserRole.Admin ? "Admin" : "Student");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var validator = new UserRegisterValidator();
            var validationResult = await validator.ValidateAsync(userRegisterDto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(userRegisterDto);
            }

            var result = await _userService.RegisterUserAsync(userRegisterDto);
            if (!result)
            {
                ModelState.AddModelError("", "Username or email already exists");
                return View(userRegisterDto);
            }

            // Auto-login after registration
            var loginDto = new UserLoginDto
            {
                Username = userRegisterDto.Username,
                Password = userRegisterDto.Password
            };

            await _userService.LoginUserAsync(loginDto);

            // Get the user to check their role
            var user = await _userService.GetUserByUsernameAsync(userRegisterDto.Username);

            // Redirect based on role
            return RedirectToAction(user.Role == UserRole.Admin ? "AdminView" : "StudentView",
                                 user.Role == UserRole.Admin ? "Admin" : "Student");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();
            return RedirectToAction("Login");
        }

    }
}
