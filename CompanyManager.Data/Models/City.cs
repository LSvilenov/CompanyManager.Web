using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Data.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}