using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Data.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<Office> Offices { get; set; } = new List<Office>();
    }
}
