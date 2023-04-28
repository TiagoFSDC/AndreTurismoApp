using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Data
{
    public class AndreTurismoAppContext : DbContext
    {
        public AndreTurismoAppContext (DbContextOptions<AndreTurismoAppContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.City> City { get; set; } = default!;
    }
}
