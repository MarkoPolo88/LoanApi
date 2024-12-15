namespace LoanApi.Models.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LoanType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Period { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; } = false;

        public User User { get; set; }
    }
}
