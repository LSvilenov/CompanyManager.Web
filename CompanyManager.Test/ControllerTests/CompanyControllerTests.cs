using CompanyManager.Services.Contracts;
using CompanyManager.Services.Models;
using CompanyManager.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CompanyManager.Test.ControllerTests
{
    public class CompanyControllerTests
    {
        [Test]
        public async Task GetByIdAsync_ShouldWork()
        {
            var officeService = new Mock<IOfficeService>();
            var companyService = new Mock<ICompanyService>();
            companyService.Setup(cs => cs.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CompanyDetailsServiceModel() { Name = "The company" });

            var employeeController = new CompaniesController(companyService.Object, officeService.Object);

            var actionResult = await employeeController.GetByIdAsync(5) as Microsoft.AspNetCore.Mvc.ObjectResult;

            Assert.IsTrue(actionResult.Value.GetType() == typeof(CompanyDetailsServiceModel));
            Assert.IsTrue((actionResult.Value as CompanyDetailsServiceModel).Name == "The company");


        }
    }
}
