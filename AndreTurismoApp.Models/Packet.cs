using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Packet
    {

        public int Id { get; set; }
        public Hotel hotel { get; set; }
        public Ticket ticket { get; set; }
        public DateTime RegisterDate { get; set; }
        public Client client { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"Id do pacote: {Id}\nHotel: {hotel}\nTicket: {ticket}" +
                $"\nData de registro do pacote: {RegisterDate}\nCliente: {client}\nPreço do pacote: {Price}";
        }
    }
}
