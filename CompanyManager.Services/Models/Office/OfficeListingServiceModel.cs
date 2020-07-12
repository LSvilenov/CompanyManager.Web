namespace CompanyManager.Services.Models
{
    public class OfficeListingServiceModel
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public int EmployeesCount { get; set; }

        public bool IsHeadQuarters { get; set; }
    }
}
