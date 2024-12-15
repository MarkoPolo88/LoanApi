using LoanApi.Data;
using LoanApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanApi.Repositories.LoanData
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLoanAsync(Loan loan)
        {
            await _context.Loan.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Loan>> GetAllLoansAsync()
        {
            return await _context.Loan.ToListAsync();
        }

        public async Task DeleteLoanAsync(Loan loan)
        {
            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<Loan> GetLoanByIdAsync(int loanId)
        {
            return await _context.Loan.FindAsync(loanId);
        }

        public async Task<List<Loan>> GetUserLoansAsync(int userId)
        {
           return await _context.Loan.Where(loan=>loan.UserId==userId).ToListAsync();
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            _context.Loan.Update(loan);
            await _context.SaveChangesAsync();
        }
    }
}
