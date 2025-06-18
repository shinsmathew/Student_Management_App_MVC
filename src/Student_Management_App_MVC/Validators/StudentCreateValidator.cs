using FluentValidation;
using Student_Management_App_MVC.Models.DTOs.Student;

namespace Student_Management_App_MVC.Validators
{
    public class StudentCreateValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateValidator()
        {
            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage("Student Name is required.")
                .MaximumLength(50).WithMessage("Student Name must not exceed 50 characters.");

            RuleFor(x => x.StudentAddress)
                .NotEmpty().WithMessage("Student Address required.")
                .MaximumLength(50).WithMessage("Student Address must not exceed 50 characters.");

            RuleFor(x => x.ZipCode)
                    .NotEmpty().WithMessage("ZipCode required.")
                    .MaximumLength(50).WithMessage("ZipCode must not exceed 50 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City required.")
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

            RuleFor(x => x.State)
                    .NotEmpty().WithMessage("State required.")
                    .MaximumLength(50).WithMessage("State must not exceed 50 characters.");

            RuleFor(x => x.Country)
                    .NotEmpty().WithMessage("Country required.")
                    .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");


            RuleFor(x => x.StudentEmail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");


            RuleFor(x => x.StudentPhone1)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").WithMessage("Invalid phone number format");

            RuleFor(x => x.StudentPhone2)
                .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$")
                .When(x => !string.IsNullOrEmpty(x.StudentPhone2)) // Only validate if value is provided
                .WithMessage("Invalid phone number format");

            RuleFor(x => x.DateOfBirth)
                 .NotEmpty().WithMessage("Date of birth is required")
                 .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past")
                 .Must(BeAValidAge).WithMessage("Student must be between 5 and 25 years old");

            RuleFor(x => x.SchoolName)
                .NotEmpty().WithMessage("SchoolName required.")
                .MaximumLength(50).WithMessage("SchoolName must not exceed 50 characters.");

            RuleFor(x => x.Course)
                    .NotEmpty().WithMessage("Course required.")
                    .MaximumLength(50).WithMessage("Course must not exceed 50 characters.");
        }

        private bool BeAValidAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;

            return age >= 5 && age <= 25;
        }
    }
}
