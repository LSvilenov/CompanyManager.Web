using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CompanyManager.Common.Constants;
using CompanyManager.Data.Models;
using Microsoft.AspNetCore.Http;

namespace CompanyManager.Web.Models
{
    public class EmployeeCreateModel
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        [Range(DataConstants.MinimalSalary, int.MaxValue)]
        public decimal Salary { get; set; }

        [Range(0, byte.MaxValue)]
        public byte VacationDays { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<int> OfficeIds { get; set; }
    }
}
