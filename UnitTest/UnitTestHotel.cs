using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.HotelService.Controllers;
using AndreTurismoApp.HotelService.Data;
using AndreTurismoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitTest
{
    public class UnitTestHotel
    {
        private DbContextOptions<AndreTurismoAppHotelServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppHotelServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                context.Hotel.Add(new Hotel { Id = 1, Name = "Ibis", Price = 1234, address = new Address { Id = 1, Street = "Street 1", ZipCode = "123456789", city = new City() { Id = 1, Description = "City1" } }, client = new Client { Id = 1, Name = "Arnaldo", Phone = "123456789", address = new Address { Id = 10, Street = "Street 1", ZipCode = "123456789", city = new City() { Id = 10, Description = "City1" } } } });
                context.Hotel.Add(new Hotel { Id = 2, Name = "Holiday Inn Express", Price = 4567, address = new Address { Id = 2, Street = "Street 2", ZipCode = "987654321", city = new City() { Id = 2, Description = "City2" } }, client = new Client { Id = 2, Name = "Cleber", Phone = "987654321", address = new Address { Id = 11, Street = "Street 2", ZipCode = "987654321", city = new City() { Id = 11, Description = "City2" } } } });
                context.Hotel.Add(new Hotel { Id = 3, Name = "Grand Mercure", Price = 8910, address = new Address { Id = 3, Street = "Street 3", ZipCode = "159647841", city = new City() { Id = 3, Description = "City3" } }, client = new Client { Id = 3, Name = "Roger", Phone = "159647841", address = new Address { Id = 12, Street = "Street 3", ZipCode = "159647841", city = new City() { Id = 12, Description = "City3" } } } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                HotelsController hotelController = new HotelsController(context);
                IEnumerable<Hotel> hotels = hotelController.GetHotel().Result.Value;

                Assert.Equal(3, hotels.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                int hotelId = 2;
                HotelsController hotelController = new HotelsController(context);
                Hotel hotel = hotelController.GetHotel(hotelId).Result.Value;
                Assert.Equal(2, hotel.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Hotel hotel = new Hotel()
            {
                Id = 4,
                Name = "Hoteis X",
                Price = 12367,
                address = new Address { Id = 6, Street = "Street 1", ZipCode = "14804440", city = new City() { Id = 6, Description = "City1" } },
                client = new Client() { Id = 4, Name = "Tiago", Phone = "14804300", address = new Address { Id = 6, Street = "Street 1", ZipCode = "14804440", city = new City() { Id = 6, Description = "City1" } } }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                HotelsController hotelController = new HotelsController(context);
                Hotel cl = hotelController.PostHotel(hotel).Result.Value;
                Assert.Equal("Hoteis X", cl.Name);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Hotel hotel = new Hotel()
            {
                Id = 2,
                Name = "Holiday Inn Express Alterado",
                address = new Address
                {
                    Id = 2,
                    Street = "Street 22 Alterada"
                }
            };


            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                HotelsController hotelController = new HotelsController(context);
                hotelController.PutHotel(2, hotel);
                Hotel ho = hotelController.GetHotel(hotel.Id).Result.Value;
                Assert.Equal("Holiday Inn Express Alterado", ho.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                HotelsController hotelController = new HotelsController(context);
                hotelController.DeleteHotel(2);
                Hotel hotel = hotelController.GetHotel(2).Result.Value;
                Assert.Null(hotel);
            }
        }
    }
}
