using System.Net;
using AndreTurismoApp.AddressService.Controllers;
using AndreTurismoApp.AddressService.Data;
using AndreTurismoApp.AddressService.Services;
using AndreTurismoApp.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AndreTurismoApp.Test
{
    public class UnitTestAddress
    {
        private DbContextOptions<AndreTurismoAppAddressServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppAddressServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                context.Address.Add(new Address { Id = 1, Street = "Street 1", ZipCode = "123456789", city = new City() { Id = 1, Description = "City1" } });
                context.Address.Add(new Address { Id = 2, Street = "Street 2", ZipCode = "987654321", city = new City() { Id = 2, Description = "City2" } });
                context.Address.Add(new Address { Id = 3, Street = "Street 3", ZipCode = "159647841", city = new City() { Id = 3, Description = "City3" } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController clientController = new AddressesController(context);
                IEnumerable<Address> clients = clientController.GetAddress().Result.Value;

                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                int clientId = 2;
                AddressesController clientController = new AddressesController(context);
                Address client = clientController.GetAddress(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Address address = new Address()
            {
                Id = 4,
                Street = "Rua 10",
                ZipCode = "14804300",
                city = new() { Id = 10, Description = "City 10" }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController clientController = new AddressesController(context);
                Address ad = clientController.PostAddress(address).Result.Value;
                Assert.Equal("Avenida Alberto Benassi", ad.Street);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Address address = new Address()
            {
                Id = 3,
                Street = "Rua 10 Alterada",
                city = new() { Id = 10, Description = "City 10 alterada" }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController clientController = new AddressesController(context);
                Address ad = (Address)clientController.PutAddress(3, address).Result;
                Assert.Equal("Rua 10 Alterada", ad.Street);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context);
                Address address = (Address)addressController.DeleteAddress(2).Result;
                Assert.Null(address);
            }
        }
    }
}