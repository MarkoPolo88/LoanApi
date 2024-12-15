using FluentValidation;
using LoanApi.Models.DTOs.Loan;

namespace LoanApi.Validators.LoanValidators
{
    public class LoanUpdateValidator : AbstractValidator<LoanUpdateDto>
    {
        public LoanUpdateValidator()
        {
            RuleFor(loan => loan.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.")
                .LessThanOrEqualTo(1000000).WithMessage("Amount cannot exceed 1,000,000.");

            RuleFor(loan => loan.Period)
                .InclusiveBetween(1, 120).WithMessage("Period must be between 1 and 120 months.");
        }
    }
}
