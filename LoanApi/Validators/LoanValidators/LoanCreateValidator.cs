using FluentValidation;
using LoanApi.Models.DTOs.Loan;

namespace LoanApi.Validators.LoanValidators
{
    public class LoanCreateValidator : AbstractValidator<LoanCreateDto>
    {
        public LoanCreateValidator()
        {
            RuleFor(loan => loan.LoanType)
                .NotEmpty().WithMessage("Loan type is required.")
                .Must(type => new[] { "QuickLoan", "AutoLoan", "Installment" }.Contains(type))
                .WithMessage("Invalid loan type.");

            RuleFor(loan => loan.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.")
                .LessThanOrEqualTo(1000000).WithMessage("Amount cannot exceed 1,000,000.");

            RuleFor(loan => loan.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Must(currency => new[] { "USD", "EUR", "GEL" }.Contains(currency))
                .WithMessage("Invalid currency.");

            RuleFor(loan => loan.Period)
                .InclusiveBetween(1, 120).WithMessage("Period must be between 1 and 120 months.");
        }
    }
}
