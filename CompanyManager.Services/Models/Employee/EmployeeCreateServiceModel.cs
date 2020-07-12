using System;
using System.Collections.Generic;

using CompanyManager.Data.Models;

using Microsoft.AspNetCore.Http;

namespace CompanyManager.Services.Models
{
    public class EmployeeCreateServiceModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        public byte VacationDays { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<int> OfficeIds { get; set; }
    }
}
