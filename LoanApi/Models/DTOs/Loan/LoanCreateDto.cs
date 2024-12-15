namespace LoanApi.Models.DTOs.Loan
{
    public class LoanCreateDto
    {
        public string LoanType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Period { get; set; }
    }
}
