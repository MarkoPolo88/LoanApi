using LoanApi.Models.DTOs.User;
using LoanApi.Models.Entities;
using LoanApi.Repositories.UserData;
using LoanApi.Services.Jwt;

namespace LoanApi.Services.user
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private object _jwtTokenGenerator;

        public UserService(IUserRepository userRepository, IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> Register(UserRegisterDto dto)
        {
            if (await _userRepository.ExistsByUsernameOrEmailAsync(dto.Username, dto.Email))
                throw new Exception("Username or Email already exists");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Age = dto.Age,
                Email = dto.Email,
                MonthlyIncome = dto.MonthlyIncome,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _userRepository.AddAsync(user);
            return "User registered successfully";
        }

        public async Task<string> Login(UserLoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password");
            }

            var role = user.Username == "admin" ? "Accountant" : "User";

            return _tokenGenerator.GenerateToken(user.Username, role, user.Id);

        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found");
            return user;
        }
    }
}
