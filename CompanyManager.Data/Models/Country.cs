using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Data.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}