namespace CompanyManager.Web.Models
{
    public class OfficeCreateModel
    {
        public int CityId { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public int CompanyId { get; set; }

        public bool IsHeadQuarters { get; set; }
    }
}
