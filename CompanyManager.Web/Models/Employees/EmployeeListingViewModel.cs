using System.Collections.Generic;

using CompanyManager.Services.Models;

namespace CompanyManager.Web.Models
{
    public class EmployeeListingViewModel
    {
        public IEnumerable<EmployeeListingServiceModel> Employees { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }
    }
}
