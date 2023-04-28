using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Address Start { get; set; }
        public Address Destination { get; set; }
        public Client client { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set;}

        public override string ToString()
        {
            return $"Id: {Id}\nEndereço de partida: {Start}\n" +
                $"Endereço de chegada: {Destination}\nCliente: {client}\nData de registro: {Date}\nPreço: {Price}";
        }
    }
}
