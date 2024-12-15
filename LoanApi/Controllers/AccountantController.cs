using LoanApi.Models.DTOs.Accountant;
using LoanApi.Services.Accountant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Accountant")]
    public class AccountantController : ControllerBase
    {
        private readonly IAccountantService _accountantService;

        public AccountantController(IAccountantService accountantService)
        {
            _accountantService = accountantService;
        }

        [HttpGet("loans")]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _accountantService.GetAllLoansAsync();
            return Ok(loans);
        }

        [HttpPut("loans/{loanId}")]
        public async Task<IActionResult> UpdateLoan(int loanId, [FromBody] AccountantLoanUpdateDto dto)
        {
            await _accountantService.UpdateLoanAsync(loanId, dto);
            return Ok("Loan updated successfully.");
        }

        [HttpDelete("loans/{loanId}")]
        public async Task<IActionResult> DeleteLoan(int loanId)
        {
            await _accountantService.DeleteLoanAsync(loanId);
            return Ok("Loan deleted successfully.");
        }

        [HttpPut("users/block/{userId}")]
        public async Task<IActionResult> BlockUser(int userId, [FromBody] AccountantUserBlockDto dto)
        {
            await _accountantService.BlockUserAsync(userId, dto);
            return Ok($"User has been {(dto.IsBlocked ? "blocked" : "unblocked")} successfully.");
        }
    }

}
