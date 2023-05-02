using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.PacketService.Data
{
    public class AndreTurismoAppPacketServiceContext : DbContext
    {
        public AndreTurismoAppPacketServiceContext (DbContextOptions<AndreTurismoAppPacketServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AndreTurismoApp.Models.Packet> Packet { get; set; } = default!;
    }
}
