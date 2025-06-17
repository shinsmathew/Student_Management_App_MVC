using FluentValidation;
using Student_Management_App_MVC.Models.DTOs.User;

namespace Student_Management_App_MVC.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("Password is required")
                 .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                 .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                 .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                 .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                 .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.ConfirmPassword)
               .Equal(x => x.Password).WithMessage("Passwords do not match");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required")
                .Must(role => role == "Admin" || role == "Student").WithMessage("Invalid role");

        }
    }
}
