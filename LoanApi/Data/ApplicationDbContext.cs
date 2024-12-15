using LoanApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loan { get; set; }
    }
}
