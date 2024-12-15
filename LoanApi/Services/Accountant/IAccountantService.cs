using LoanApi.Models.DTOs.Accountant;
using LoanApi.Models.DTOs.Loan;

namespace LoanApi.Services.Accountant
{
    public interface IAccountantService
    {
        Task<List<LoanViewDto>> GetAllLoansAsync();
        Task UpdateLoanAsync(int loanId, AccountantLoanUpdateDto dto);
        Task DeleteLoanAsync(int loanId);
        Task BlockUserAsync(int userId, AccountantUserBlockDto dto);
    }
}

