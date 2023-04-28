using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;
using AndreTurismoApp.Models.DTO;

namespace AndreTurismoApp.AddressService.Data
{
    public class AndreTurismoAppAddressServiceContext : DbContext
    {
        public AndreTurismoAppAddressServiceContext (DbContextOptions<AndreTurismoAppAddressServiceContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressDTO>().HasNoKey();
        }
        public DbSet<AndreTurismoApp.Models.Address> Address { get; set; } = default!;

        public DbSet<AndreTurismoApp.Models.DTO.AddressDTO>? AddressDTO { get; set; }
    }
}
