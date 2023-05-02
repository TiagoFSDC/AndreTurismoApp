using System.Data;
using System.Net;
using System.Net.Http.Json;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.Services
{
    public class AddressService
    {
        static readonly HttpClient addressClient = new HttpClient();
        public async Task<List<Address>> GetAddress()
        {
            try
            {
                HttpResponseMessage response = await AddressService.addressClient.GetAsync("https://localhost:7221/api/Addresses");
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Address>>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Address> GetAddressById(int id)
        {
            try
            {
                HttpResponseMessage response = await AddressService.addressClient.GetAsync("https://localhost:7221/api/Addresses/" + id);
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
        public async Task<Address> PostAddress(Address a)
        {
            try
            {
                HttpResponseMessage response = await AddressService.addressClient.PostAsJsonAsync("https://localhost:7221/api/Addresses/", a);
                response.EnsureSuccessStatusCode();
                string address = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Address>(address);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> PutAddress(int id, Address a)
        {
            try
            {
                HttpResponseMessage response = await AddressService.addressClient.PutAsJsonAsync("https://localhost:7221/api/Addresses/" + id , a);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> DeleteAddress(int id)
        {
            try
            {
                HttpResponseMessage response = await AddressService.addressClient.DeleteAsync("https://localhost:7221/api/Addresses/" + id);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
