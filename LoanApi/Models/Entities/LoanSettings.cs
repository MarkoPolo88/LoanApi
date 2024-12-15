namespace LoanApi.Models.Entities
{
    public class LoanSettings
    {
        public List<string> LoanTypes { get; set; }
        public List<string> Currencies { get; set; }
        public List<string> Statuses { get; set; }
        public int DefaultPeriod { get; set; }
    }
}
