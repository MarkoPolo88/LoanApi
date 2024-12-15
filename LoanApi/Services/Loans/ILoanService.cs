using LoanApi.Models.DTOs.Loan;

namespace LoanApi.Services.Loans
{
    public interface ILoanService
    {
        Task<List<LoanViewDto>> GetUserLoansAsync(int userId);
        Task<LoanViewDto> GetUserLoanAsync(int LoanId);
        Task AddLoanAsync(int userId, LoanCreateDto loanCreateDto);
        Task UpdateLoanAsync(int loanId, int userId, LoanUpdateDto loanUpdateDto);
        Task DeleteLoanAsync(int loanId, int userId);
    }
}
