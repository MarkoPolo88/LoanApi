using LoanApi.Models.Entities;

namespace LoanApi.Repositories.LoanData
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetUserLoansAsync(int userId);
        Task<List<Loan>> GetAllLoansAsync();
        Task<Loan> GetLoanByIdAsync(int loanId);
        Task AddLoanAsync(Loan loan);
        Task UpdateLoanAsync(Loan loan);
        Task DeleteLoanAsync(Loan loan);
    }
}
