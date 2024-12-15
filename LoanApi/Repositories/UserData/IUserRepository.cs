using LoanApi.Models.Entities;

namespace LoanApi.Repositories.UserData
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<bool> ExistsByUsernameOrEmailAsync(string username, string email);
        Task AddAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
