using CompanyManager.Common.Constants;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        [Range(DataConstants.MinimalSalary, int.MaxValue)] // Max range with bonuses :)
        public decimal Salary { get; set; }

        [Range(0, byte.MaxValue)]
        public byte VacationDays { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public string Image { get; set; }

        public ICollection<OfficeEmployee> OfficeEmployees { get; set; } = new List<OfficeEmployee>();
    }
}
