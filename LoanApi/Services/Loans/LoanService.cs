using LoanApi.Models.DTOs.Loan;
using LoanApi.Models.Entities;
using LoanApi.Repositories.LoanData;
using LoanApi.Repositories.UserData;
using Microsoft.Extensions.Options;

namespace LoanApi.Services.Loans
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOptions<LoanSettings> _loanSettings;

        public LoanService(ILoanRepository loanRepository, IOptions<LoanSettings> loanSettings, IUserRepository userRepository)
        {
            _loanRepository = loanRepository;
            _loanSettings = loanSettings;
            _userRepository = userRepository;
        }
        public async Task AddLoanAsync(int userId, LoanCreateDto loanCreateDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user.IsBlocked)
            {
                throw new ArgumentException("User is Blocked");
            }
            if (!_loanSettings.Value.LoanTypes.Contains(loanCreateDto.LoanType) ||
            !_loanSettings.Value.Currencies.Contains(loanCreateDto.Currency))
            {
                throw new ArgumentException("Invalid loan type or currency.");
            }
            var loan = new Loan
            {
                UserId = userId,
                LoanType = loanCreateDto.LoanType,
                Amount = loanCreateDto.Amount,
                Currency = loanCreateDto.Currency,
                Period = loanCreateDto.Period,
                Status = "Processing"

            };
            await _loanRepository.AddLoanAsync(loan);
        }

        public async Task DeleteLoanAsync(int loanId, int userId)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(loanId);

            if (loan == null || loan.UserId != userId || loan.Status != "Processing")
            {
                throw new InvalidOperationException("Loan cannot be deleted.");
            }

            await _loanRepository.DeleteLoanAsync(loan);
        }

        public async Task<List<LoanViewDto>> GetUserLoansAsync(int userId)
        {
            var loans = await _loanRepository.GetUserLoansAsync(userId);
            return loans.Select(loan=> new LoanViewDto
            {
                Id=loan.Id,
                LoanType=loan.LoanType,
                Amount=loan.Amount,
                Currency=loan.Currency,
                Period=loan.Period,
                Status=loan.Status,

            }).ToList();
        }

        public async Task<LoanViewDto> GetUserLoanAsync(int loanId)
        {

            var loan = await _loanRepository.GetLoanByIdAsync(loanId);
            if (loan == null)
            {
                throw new Exception("Loan not found.");
            }

            // Map the loan entity to LoanViewDto
            var loanViewDto = new LoanViewDto
            {
                Id = loan.Id,
                LoanType = loan.LoanType,
                Amount = loan.Amount,
                Currency = loan.Currency,
                Period = loan.Period,
                Status = loan.Status,
            };

            return loanViewDto;

        }

        public async Task UpdateLoanAsync(int loanId, int userId, LoanUpdateDto loanUpdateDto)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(loanId);
            if (loan == null || loan.UserId != userId || loan.Status != "Processing")
            {
                throw new InvalidOperationException("Loan cannot be updated.");
            }
            loan.Amount = loanUpdateDto.Amount;
            loan.Period = loanUpdateDto.Period;
            await _loanRepository.UpdateLoanAsync(loan);

        }
    }
}
