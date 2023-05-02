using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models.DTO;

namespace AndreTurismoApp.Models
{
    public class Address
    {
        public readonly static string INSERT = @"insert into Endereco (Logradouro, Numero, Bairro, CEP, Complemento, IdCidade)
                                                    values (@Street, @Number, @District, @ZipCode, @Complement, @IdCidade)";

        public readonly static string GETALL = @"select e.Id,e.Logradouro as Street,e.Numero as Number, e.Bairro as District, e.CEP as ZipCode, e.Complemento as Complement, 
                                                  e.Dtcadastro as RegisterDate, c.Id as Id, c.Descricao as Description, c.Dtcadastro as RegisterDate
                                                 from Endereco e Join Cidade c ON e.IdCidade = c.Id";

        public readonly static string UPDATE = @"update Endereco Set Logradouro = @Street where Id = @Id";
        public readonly static string DELETE = @"Delete from Endereco where Id = @Id";

        public Address()
        {

        }
        
        public int Id { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public string? District { get; set; }
        public string ZipCode { get; set; }
        public string? Complement { get; set; }
        public City city { get; set; }
        public DateTime RegisterDate { get; set; }

        public Address(AddressDTO addressDTO)
        {
            this.Street = addressDTO.Logradouro;
            this.District = addressDTO.Bairro;
            this.Complement = addressDTO.Complemento;
            this.ZipCode = addressDTO.CEP;
            this.city = new City() { Description = addressDTO.Cidade };
        }

        public override string ToString()
        {
            return $"\nId: {Id}\nLogradouro: {Street}\nNumero: {Number}\nBairro: {District}\nCEP: {ZipCode}" +
                $"\nComplemento: {Complement}\nData: {RegisterDate}\nCidade: {city}";
        }
    }
}
