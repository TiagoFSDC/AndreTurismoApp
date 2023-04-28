using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AndreTurismoApp.Models.DTO
{
    public class AddressDTO
    {
        [JsonProperty("pais")]
        public string? Country { get; set; }

        [JsonProperty("cep")]
        public string CEP { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }

        [JsonProperty("uf")]
        public string State { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }
    }
}
