﻿namespace LoanApi.Models.DTOs.User
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string Password { get; set; }
    }

}
