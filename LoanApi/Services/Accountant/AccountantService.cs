using LoanApi.Models.DTOs.Accountant;
using LoanApi.Models.DTOs.Loan;
using LoanApi.Repositories.LoanData;
using LoanApi.Repositories.UserData;

namespace LoanApi.Services.Accountant
{

    public class AccountantService : IAccountantService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;

        public AccountantService(ILoanRepository loanRepository, IUserRepository userRepository)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
        }

        public async Task<List<LoanViewDto>> GetAllLoansAsync()
        {
            var loans = await _loanRepository.GetAllLoansAsync();
            return loans.Select(loan => new LoanViewDto
            {
                LoanType = loan.LoanType,
                Amount = loan.Amount,
                Currency = loan.Currency,
                Period = loan.Period,
                Status = loan.Status
            }).ToList();
        }

        public async Task UpdateLoanAsync(int loanId, AccountantLoanUpdateDto dto)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(loanId);
            if (loan == null) throw new Exception("Loan not found");

            loan.Status = dto.Status;
            loan.Amount = dto.Amount;

            await _loanRepository.UpdateLoanAsync(loan);
        }

        public async Task DeleteLoanAsync(int loanId)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(loanId);
            if (loan == null) throw new Exception("Loan not found");

            await _loanRepository.DeleteLoanAsync(loan);
        }

        public async Task BlockUserAsync(int userId, AccountantUserBlockDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            user.IsBlocked = dto.IsBlocked;
            await _userRepository.UpdateUserAsync(user);
        }
    }

}
