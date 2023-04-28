using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AndreTurismoApp.Models
{
    public class Client
    {
        public readonly static string INSERT = @"insert into Client (Name, Phone, IdEndereco)
                                                    values (@Name, @Phone, @IdEndereco)";

        public readonly static string GETALL = @"select c.Name,c.Phone,c.RegisterDate, e.Id, e.Logradouro,
                                                    e.Numero, e.Bairro, e.CEP, e.Complemento,e.DtCadastro ,
                                                    cid.Id, cid.Descricao, cid.DtCadastro as data
                                                    from Endereco e 
                                                    Join Client as c ON e.Id = c.IdEndereco
                                                    Join Cidade as cid ON cid.Id = e.IdCidade";

        public readonly static string UPDATE = "update Client Set Name = @Name where Id = @Id";
        public readonly static string DELETE = "Delete from Client where Id = @Id";

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address address { get; set; } 
        public DateTime RegisterDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nNome: {Name}\nTelefone: {Phone}\nEndereço: {address}\nData de registro: {RegisterDate}\n";
        }
    }
}
