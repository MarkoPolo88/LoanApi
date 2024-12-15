using LoanApi.Filters;
using LoanApi.Models.DTOs.Loan;
using LoanApi.Models.Entities;
using LoanApi.Services.Loans;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LoanApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController:ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLoans()
        {

            var userId = User.FindFirst("id")?.Value ?? throw new UnauthorizedAccessException("ID claim is missing.");
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { error = "Invalid token: 'id' claim is missing." });
            }

            int userIdInt = int.Parse(userId);
            var loans = await _loanService.GetUserLoansAsync(userIdInt);
            return Ok(loans);
        }

        [HttpPost]
        public async Task<IActionResult> AddLoan([FromBody] LoanCreateDto loan)
        {
            var idClaim = User.FindFirst("id") ?? throw new UnauthorizedAccessException("ID claim is missing.");
            Console.WriteLine(idClaim);
            if (idClaim == null)
            {
                return Unauthorized("Invalid token: 'id' claim is missing.");
            }
            var userId = int.Parse(idClaim.Value);

            await _loanService.AddLoanAsync(userId, loan);
            return Ok();
        }

        [HttpPut("{loanId}")]
        public async Task<IActionResult> UpdateLoan(int loanId, [FromBody] LoanUpdateDto updatedLoan)
        {
            var userId = int.Parse(User.FindFirst("id").Value ?? throw new UnauthorizedAccessException("ID claim is missing."));

            await _loanService.UpdateLoanAsync(loanId, userId, updatedLoan);
            var loans = await _loanService.GetUserLoansAsync(userId);

            return Ok(loans);
        }

        [HttpDelete("{loanId}")]
        public async Task<IActionResult> DeleteLoan(int loanId)
        {
            var userId = int.Parse(User.FindFirst("id").Value ?? throw new UnauthorizedAccessException("ID claim is missing."));
            await _loanService.DeleteLoanAsync(loanId, userId);
            return Ok("deleted");
        }

    }
}
