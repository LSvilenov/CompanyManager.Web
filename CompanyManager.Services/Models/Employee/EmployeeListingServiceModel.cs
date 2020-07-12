using System;

namespace CompanyManager.Services.Models
{
    public class EmployeeListingServiceModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public string ExperienceLevel { get; set; }
    }
}
