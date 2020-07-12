using System.Collections.Generic;

using CompanyManager.Data.Models;

namespace CompanyManager.Services.Models
{
    public class OfficeDetailsServiceModel
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int CityId { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public bool IsHeadQuarters { get; set; }

        //public IEnumerable<Document> Documents { get; set; }

        //public IEnumerable<EmployeeListingServiceModel> Employees { get; set; }
    }
}
