using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Data.Models
{
    [Owned]
    public class Address
    {
        [Required]
        [MinLength(2)]
        public string Street { get; set; }

        [Range(0, 1000)]
        public int StreetNumber { get; set; }
    }
}
