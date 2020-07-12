using System.Collections.Generic;

using CompanyManager.Services.Models;

namespace CompanyManager.Web.Models
{
    public class CompanyListingViewModel
    {
        public IEnumerable<CompanyListingServiceModel> Companies { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }
    }
}
