using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CompanyManager.Data;
using CompanyManager.Data.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Web.Infrastructure
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder appBuilder)
            => appBuilder.SeedDataAsync().GetAwaiter().GetResult();

        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var dbContext = services.GetService<ApplicationDbContext>();

                await dbContext.Database.MigrateAsync();

                if (dbContext.Countries.Any())
                {
                    return appBuilder;
                }

                List<Country> countries = new List<Country>
                {
                    new Country { Name = "Bulgaria" },
                    new Country { Name = "Germany" },
                    new Country { Name = "Austria" },
                    new Country { Name = "Switzerland" }
                };

                dbContext.Countries.AddRange(countries);

                await dbContext.SaveChangesAsync();

                List<City> cities = new List<City>
                {
                    new City { CountryId = countries[0].Id, Name = "Sofia" },
                    new City { CountryId = countries[0].Id, Name = "Plovdiv" },
                    new City { CountryId = countries[0].Id, Name = "Varna" },
                    new City { CountryId = countries[1].Id, Name = "Karlsruhe" },
                    new City { CountryId = countries[1].Id, Name = "Berlin" },
                    new City { CountryId = countries[1].Id, Name = "Munich" },
                    new City { CountryId = countries[2].Id, Name = "Vienna" },
                    new City { CountryId = countries[2].Id, Name = "Salzburg" },
                    new City { CountryId = countries[2].Id, Name = "Innsbruck" },
                    new City { CountryId = countries[3].Id, Name = "Zürich" },
                    new City { CountryId = countries[3].Id, Name = "Basel" },
                    new City { CountryId = countries[3].Id, Name = "Geneva" },
                };

                dbContext.Cities.AddRange(cities);

                await dbContext.SaveChangesAsync();

                List<Company> companies = new List<Company>
                {
                    new Company { Name = "STP", CreationDate = DateTime.Now.AddYears(-20) },
                    new Company { Name = "Some other company", CreationDate = DateTime.Now.AddYears(-5) }
                };

                dbContext.Companies.AddRange(companies);

                await dbContext.SaveChangesAsync();

                List<Office> offices = new List<Office>
                {
                    new Office { CityId = cities[0].Id, CompanyId = companies[0].Id, IsHeadQuarters = true, Address = new Address { Street = "Some street", StreetNumber = 5 } }
                };

                dbContext.Offices.AddRange(offices);

                await dbContext.SaveChangesAsync();

                List<Employee> employees = new List<Employee>
                {
                    new Employee { FirstName = "Lyubomir", LastName = "Svilenov", ExperienceLevel = ExperienceLevel.Mid, Salary = 100000, StartingDate = new DateTime(2020, 9, 1) }
                };

                dbContext.Employees.AddRange(employees);

                await dbContext.SaveChangesAsync();

                employees[0].OfficeEmployees.Add(new OfficeEmployee { OfficeId = offices[0].Id, EmployeeId = employees[0].Id });

                await dbContext.SaveChangesAsync();
            }

            return appBuilder;
        }
    }
}
