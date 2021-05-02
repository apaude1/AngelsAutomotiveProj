using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data
{
    //This class automatically populates the database with data, without the need to insert data manualy. Only applies when the database is empty.
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;//automatically generates random numbers to fill in the data


        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();//checks if the database is created

            await _userHelper.CheckRoleAsync("Admin");//check if these roles already exist
            await _userHelper.CheckRoleAsync("Customer");


            if (!_context.States.Any())
            {
                var cities = new List<City>();
                cities.Add(new City {Name = "Baltimore" });
                cities.Add(new City { Name = "Towson" });
                cities.Add(new City { Name = "Annapolis" });
                cities.Add(new City { Name = "Timonium" });

                _context.States.Add(new State
                {
                    Cities = cities,
                    Name = "Maryland"
                });

                await _context.SaveChangesAsync();
            }

            var user = await _userHelper.GetUserByEmailAsync("js8007163@gmail.com");
            if(user == null)
            {
                user = new User
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "js8007163@gmail.com",
                    UserName = "js8007163@gmail.com",
                    PhoneNumber = "1243456889",
                    Address = "test",
                    CityId = _context.States.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.States.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(user, "Virginia#2020");//creates a user with the data and password(Usually this user is the admin)
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            if (!_context.Vehicles.Any())
            {
                this.AddVehicle("48123658489411439", 1, 2020,"Toyota", "Camry", "01-01-AA", 1000, user);
                this.AddVehicle("4Y1SL65848Z411439", 2, 2021, "Honda", "Accord", "02-02-BB", 2000, user);
                this.AddVehicle("19UYA31581L000000", 3, 2020, "Nissan", "Rogue", "03-03-CC", 3000, user);
                await _context.SaveChangesAsync();
            }
        }


        private void AddVehicle(string VinNumber, int Id, int VehYear, string VehMake, string vehModel, string VehiclePlateNumber, int VehMileage, User user)
        {
            _context.Vehicles.Add(new Vehicle
            {
                VinNummber = VinNumber,
                Id = Id,
                VehYear = VehYear,
                VehMake = VehMake,
                VehModel = vehModel,
                VehiclePlateNumber = VehiclePlateNumber,
                VehMileage = VehMileage,
                //Color = color,
                //Fuel = fuel,
                User = user
            });
        }
    }
}
