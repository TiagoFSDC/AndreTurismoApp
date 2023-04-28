using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Hotel
    {
        internal Client client;

        public int Id { get; set; }
        public string Name { get; set; }
        public Address address { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Id do hotel: {Id}\nNome do hotel: {Name}\n{address}\nData de registro: {RegisterDate}\nPreço do hotel";
        }
    }
}
