using System;
using System.Collections.Generic;

namespace CompanyManager.Services.Models
{
    public class CompanyListingServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public int OfficesCount { get; set; }
    }
}
