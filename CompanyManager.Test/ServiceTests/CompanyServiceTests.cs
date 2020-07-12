using CompanyManager.Data;
using CompanyManager.Data.Models;
using CompanyManager.Services;
using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManager.Test.ServiceTests
{
    public class CompanyServiceTests
    {
        private ApplicationDbContext context;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CompanyMemoryDatabase").Options;

            context = new ApplicationDbContext(options);

            var companies = new List<Company>
            {
                new Company() { Name = "MyCompany 1", CreationDate = new DateTime(2011, 11, 11)},
                new Company() { Name = "MyCompany 2", CreationDate = new DateTime(2012, 12, 12)},
                new Company() { Name = "MyCompany 3", CreationDate = DateTime.UtcNow }
            };

            context.Companies.AddRange(companies);

            await context.SaveChangesAsync();
        }

        [Test]
        public async Task GetAllCompanies_ShouldWork()
        {
            var companyService = new CompanyService(context);

            var companies = await companyService.GetAllAsync(string.Empty);

            Assert.AreEqual(3, companies.Count());
            Assert.IsTrue(companies.Count(c => c.Name.Contains("MyCompany"))== 3);
        }

        [Test]
        public async Task GetAllWithFilter_ShouldWork()
        {
            var companyService = new CompanyService(context);

            var companies = await companyService.GetAllAsync("MyCompany 1");

            Assert.AreEqual(1, companies.Count());
            Assert.IsTrue(companies.First().Name == "MyCompany 1");
        }

        [Test]
        public async Task Delete_ShouldWork()
        {
            var companyService = new CompanyService(context);

            var companies = await companyService.GetAllAsync(string.Empty);

            int countBeforeDelete = companies.Count();
            int idOfDeletedCompany = companies.First().Id;

            await companyService.DeleteAsync(idOfDeletedCompany);

            companies = await companyService.GetAllAsync(string.Empty);

            Assert.AreEqual(countBeforeDelete - 1, companies.Count());
            Assert.IsNull(companies.FirstOrDefault(c => c.Id == idOfDeletedCompany));
        }
    }
}
