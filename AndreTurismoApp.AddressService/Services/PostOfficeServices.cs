using AndreTurismoApp.Models.DTO;
using Newtonsoft.Json;

namespace AndreTurismoApp.AddressService.Services
{
    public class PostOfficeServices
    {
        static readonly HttpClient endereco = new HttpClient();
        public static async Task<AddressDTO> GetAddress(string cep)
        {
            try
            {
                HttpResponseMessage response = await PostOfficeServices.endereco.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                response.EnsureSuccessStatusCode();
                string ender = await response.Content.ReadAsStringAsync();
                var end = JsonConvert.DeserializeObject<AddressDTO>(ender);
                return end;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
