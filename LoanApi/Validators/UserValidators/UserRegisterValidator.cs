using FluentValidation;
using LoanApi.Models.DTOs.User;

namespace LoanApi.Validators.UserValidators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");
            RuleFor(x => x.LastName)
                 .NotEmpty().WithMessage("Last name is required")
                 .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(5).WithMessage("Username must be at least 5 characters long");

            RuleFor(x => x.Age)
                .InclusiveBetween(18, 120).WithMessage("Age must be between 18 and 120");
            RuleFor(x => x.Email)
                      .NotEmpty().WithMessage("Email is required")
                      .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.MonthlyIncome)
                .GreaterThanOrEqualTo(0).WithMessage("Monthly income must be greater than or equal to 0");

            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("Password is required")
                 .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                 .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                 .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                 .Matches("[0-9]").WithMessage("Password must contain at least one number")
                 .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

        }
    }
}
