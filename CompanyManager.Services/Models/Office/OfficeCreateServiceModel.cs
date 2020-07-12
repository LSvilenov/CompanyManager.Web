using CompanyManager.Data.Models;

namespace CompanyManager.Services.Models
{
    public class OfficeCreateServiceModel
    {
        public int Id { get; set; }

        public Address Address { get; set; }

        public int CompanyId { get; set; }

        public int CityId { get; set; }

        public bool IsHeadQuarters { get; set; }
    }
}
