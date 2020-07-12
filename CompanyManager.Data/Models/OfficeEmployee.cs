namespace CompanyManager.Data.Models
{
    public class OfficeEmployee
    {
        public int OfficeId { get; set; }

        public Office Office { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
