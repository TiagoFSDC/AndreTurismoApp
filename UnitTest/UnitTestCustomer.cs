using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.ClientService.Controllers;
using AndreTurismoApp.ClientService.Data;
using AndreTurismoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitTest
{
    public class UnitTestCustomer
    {
        private DbContextOptions<AndreTurismoAppClientServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppClientServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                context.Client.Add(new Client { Id = 1, Name = "Arnaldo", Phone = "123456789", address = new Address { Id = 1, Street = "Street 1", ZipCode = "123456789", city = new City() { Id = 1, Description = "City1" } } });
                context.Client.Add(new Client { Id = 2, Name = "Cleber", Phone = "987654321", address = new Address { Id = 2, Street = "Street 2", ZipCode = "987654321", city = new City() { Id = 2, Description = "City2" } } });
                context.Client.Add(new Client { Id = 3, Name = "Roger", Phone = "159647841", address = new Address { Id = 3, Street = "Street 3", ZipCode = "159647841", city = new City() { Id = 3, Description = "City3" } } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                ClientsController clientController = new ClientsController(context);
                IEnumerable<Client> clients = clientController.GetClient().Result.Value;

                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                int clientId = 2;
                ClientsController clientController = new ClientsController(context);
                Client client = clientController.GetClient(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Client client = new Client()
            {
                Id = 4,
                Name = "Tiago",
                Phone = "14804300",
                address = new Address { Id = 6, Street = "Street 1", ZipCode = "14804440", city = new City() { Id = 6, Description = "City1" } }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                ClientsController clientController = new ClientsController(context);
                Client cl = clientController.PostClient(client).Result.Value;
                Assert.Equal("Tiago", cl.Name);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Client client = new Client()
            {
                Id = 3,
                Name = "Cleber Alterado",
                address = new Address
                {
                    Id = 1,
                    Street = "Street 11 Alterada"
                }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                ClientsController clientController = new ClientsController(context);
                clientController.PutClient(3, client);
                Client cl = clientController.GetClient(client.Id).Result.Value;
                Assert.Equal("Cleber Alterado", cl.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                ClientsController ClientController = new ClientsController(context);
                ClientController.DeleteClient(2);
                Client client = ClientController.GetClient(2).Result.Value;
                Assert.Null(client);
            }
        }
    }
}
