using CompanyManager.Services.Models;

using System.Collections.Generic;

namespace CompanyManager.Web.Models
{
    public class OfficeListingViewModel
    {
        public IEnumerable<OfficeListingServiceModel> Offices { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }
    }
}
