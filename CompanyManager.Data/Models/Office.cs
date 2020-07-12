using System.Collections.Generic;

namespace CompanyManager.Data.Models
{
    public class Office
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public Address Address { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public bool IsHeadQuarters { get; set; }

        public ICollection<Document> Documents { get; set; } = new List<Document>();

        public ICollection<OfficeEmployee> OfficeEmployees { get; set; } = new List<OfficeEmployee>();
    }
}
