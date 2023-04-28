using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class City
    {
        public readonly static string INSERT = "insert into Cidade (Descricao) values (@Description)";
        public readonly static string GETALL = "select Id, Descricao as Description, Dtcadastro as RegisterDate from Cidade";
        public readonly static string UPDATE = "update Cidade Set Descricao =  @Description where id = @Id";
        public readonly static string DELETE = "Delete from Cidade where Id = @Id";
        
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime RegisterDate { get; set; }

        //public override string ToString()
        //{
        //    return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        //}

        public override string ToString()
        {
            return $"Id: {Id}\nDescrição: {Description}\nData de registro da cidade: {RegisterDate}";
        }
    }
}
