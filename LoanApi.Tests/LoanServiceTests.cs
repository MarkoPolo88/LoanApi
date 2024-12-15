using Xunit;
using Moq;
using LoanApi.Services.Loans;
using LoanApi.Repositories.LoanData;
using LoanApi.Repositories.UserData;
using LoanApi.Models.DTOs.Loan;
using LoanApi.Models.Entities;
using Microsoft.Extensions.Options;

namespace LoanApi.Tests.Services
{
    public class LoanServiceTests
    {
        private readonly Mock<ILoanRepository> _mockLoanRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IOptions<LoanSettings>> _mockLoanSettings;
        private readonly LoanService _loanService;

        public LoanServiceTests()
        {
            _mockLoanRepository = new Mock<ILoanRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockLoanSettings = new Mock<IOptions<LoanSettings>>();

            // LoanSettings configuration
            _mockLoanSettings.Setup(opt => opt.Value).Returns(new LoanSettings
            {
                LoanTypes = new List<string> { "Installment", "Rapid Loan" },
                Currencies = new List<string> { "USD", "EUR" }
            });

            _loanService = new LoanService(_mockLoanRepository.Object, _mockLoanSettings.Object, _mockUserRepository.Object);
        }

        #region AddLoanAsync Tests

        [Fact]
        public async Task AddLoanAsync_ShouldAddLoan_WhenLoanDataIsValid()
        {
            // Arrange
            var userId = 1;
            var loanCreateDto = new LoanCreateDto
            {
                LoanType = "Installment",
                Amount = 500,
                Currency = "USD",
                Period = 12
            };
            var user = new User { Id = userId, IsBlocked = false };

            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _mockLoanRepository.Setup(repo => repo.AddLoanAsync(It.IsAny<Loan>())).Returns(Task.CompletedTask);

            // Act
            await _loanService.AddLoanAsync(userId, loanCreateDto);

            // Assert
            _mockLoanRepository.Verify(repo => repo.AddLoanAsync(It.IsAny<Loan>()), Times.Once);
        }

        [Fact]
        public async Task AddLoanAsync_ShouldThrowArgumentException_WhenUserIsBlocked()
        {
            // Arrange
            var userId = 1;
            var loanCreateDto = new LoanCreateDto { LoanType = "Installment", Amount = 500, Currency = "USD", Period = 12 };
            var blockedUser = new User { Id = userId, IsBlocked = true };

            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(blockedUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _loanService.AddLoanAsync(userId, loanCreateDto));
            Assert.Equal("User is Blocked", exception.Message);

            _mockLoanRepository.Verify(repo => repo.AddLoanAsync(It.IsAny<Loan>()), Times.Never);
        }

        #endregion

        #region DeleteLoanAsync Tests

        [Fact]
        public async Task DeleteLoanAsync_ShouldDeleteLoan_WhenLoanIsProcessing()
        {
            // Arrange
            var loanId = 1;
            var userId = 1;
            var loan = new Loan { Id = loanId, UserId = userId, Status = "Processing" };

            _mockLoanRepository.Setup(repo => repo.GetLoanByIdAsync(loanId)).ReturnsAsync(loan);

            // Act
            await _loanService.DeleteLoanAsync(loanId, userId);

            // Assert
            _mockLoanRepository.Verify(repo => repo.DeleteLoanAsync(loan), Times.Once);
        }

        [Fact]
        public async Task DeleteLoanAsync_ShouldThrowInvalidOperationException_WhenLoanIsNotProcessing()
        {
            // Arrange
            var loanId = 1;
            var userId = 1;
            var loan = new Loan { Id = loanId, UserId = userId, Status = "Approved" };

            _mockLoanRepository.Setup(repo => repo.GetLoanByIdAsync(loanId)).ReturnsAsync(loan);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _loanService.DeleteLoanAsync(loanId, userId));
            Assert.Equal("Loan cannot be deleted.", exception.Message);

            _mockLoanRepository.Verify(repo => repo.DeleteLoanAsync(It.IsAny<Loan>()), Times.Never);
        }

        #endregion

        #region GetUserLoansAsync Tests

        [Fact]
        public async Task GetUserLoansAsync_ShouldReturnLoans_WhenLoansExist()
        {
            // Arrange
            var userId = 1;
            var loans = new List<Loan>
            {
                new Loan { Id = 1, UserId = userId, LoanType = "Installment", Amount = 500, Currency = "USD", Status = "Processing" }
            };

            _mockLoanRepository.Setup(repo => repo.GetUserLoansAsync(userId)).ReturnsAsync(loans);

            // Act
            var result = await _loanService.GetUserLoansAsync(userId);

            // Assert
            Assert.Single(result);
            Assert.Equal(500, result.First().Amount);
        }

        #endregion

        #region UpdateLoanAsync Tests

        [Fact]
        public async Task UpdateLoanAsync_ShouldUpdateLoan_WhenLoanIsProcessing()
        {
            // Arrange
            var loanId = 1;
            var userId = 1;
            var loanUpdateDto = new LoanUpdateDto { Amount = 1000, Period = 24 };
            var loan = new Loan { Id = loanId, UserId = userId, Status = "Processing" };

            _mockLoanRepository.Setup(repo => repo.GetLoanByIdAsync(loanId)).ReturnsAsync(loan);

            // Act
            await _loanService.UpdateLoanAsync(loanId, userId, loanUpdateDto);

            // Assert
            _mockLoanRepository.Verify(repo => repo.UpdateLoanAsync(It.Is<Loan>(l => l.Amount == 1000 && l.Period == 24)), Times.Once);
        }

        [Fact]
        public async Task UpdateLoanAsync_ShouldThrowInvalidOperationException_WhenLoanIsNotProcessing()
        {
            // Arrange
            var loanId = 1;
            var userId = 1;
            var loanUpdateDto = new LoanUpdateDto { Amount = 1000, Period = 24 };
            var loan = new Loan { Id = loanId, UserId = userId, Status = "Approved" };

            _mockLoanRepository.Setup(repo => repo.GetLoanByIdAsync(loanId)).ReturnsAsync(loan);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _loanService.UpdateLoanAsync(loanId, userId, loanUpdateDto));
            Assert.Equal("Loan cannot be updated.", exception.Message);

            _mockLoanRepository.Verify(repo => repo.UpdateLoanAsync(It.IsAny<Loan>()), Times.Never);
        }

        #endregion
    }
}
