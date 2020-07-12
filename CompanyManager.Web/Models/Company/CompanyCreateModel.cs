using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Web.Models
{
    public class CompanyCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
