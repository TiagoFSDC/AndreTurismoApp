using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Net;

namespace AndreTurismoApp.Services
{
    public class CustomerService
    {
        static readonly HttpClient clientClient = new HttpClient();
        public async Task<List<Client>> GetClient()
        {
            try
            {
                HttpResponseMessage response = await CustomerService.clientClient.GetAsync("https://localhost:7234/api/Clients");
                response.EnsureSuccessStatusCode();
                string client = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Client>>(client);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Client> GetClientById(int id)
        {
            try
            {
                HttpResponseMessage response = await CustomerService.clientClient.GetAsync("https://localhost:7234/api/Clients/" + id);
                response.EnsureSuccessStatusCode();
                string client = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Client>(client);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
        public async Task<Client> PostClient(Client a)
        {
            try
            {
                HttpResponseMessage response = await CustomerService.clientClient.PostAsJsonAsync("https://localhost:7234/api/Clients/", a);
                response.EnsureSuccessStatusCode();
                string client = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Client>(client);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> PutClient(int id, Client a)
        {
            try
            {
                HttpResponseMessage response = await CustomerService.clientClient.PutAsJsonAsync("https://localhost:7234/api/Clients/" + id, a);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> DeleteClient(int id)
        {
            try
            {
                HttpResponseMessage response = await CustomerService.clientClient.DeleteAsync("https://localhost:7234/api/Clients/" + id);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
