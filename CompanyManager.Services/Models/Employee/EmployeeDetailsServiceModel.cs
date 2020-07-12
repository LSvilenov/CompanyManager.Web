using System;

namespace CompanyManager.Services.Models
{
    public class EmployeeDetailsServiceModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        public int VacationDays { get; set; }

        public string ExperienceLevel { get; set; }

        public string Image { get; set; }
    }
}