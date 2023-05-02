using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Net;

namespace AndreTurismoApp.Services
{
    public class TicketService
    {
        static readonly HttpClient TicketClient = new HttpClient();
        public async Task<List<Ticket>> GetTicket()
        {
            try
            {
                HttpResponseMessage response = await TicketService.TicketClient.GetAsync("https://localhost:7106/api/Tickets");
                response.EnsureSuccessStatusCode();
                string ticket = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Ticket>>(ticket);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            try
            {
                HttpResponseMessage response = await TicketService.TicketClient.GetAsync("https://localhost:7106/api/Tickets/" + id);
                response.EnsureSuccessStatusCode();
                string ticket = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket>(ticket);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
        public async Task<Ticket> PostTicket(Ticket ticket1)
        {
            try
            {
                HttpResponseMessage response = await TicketService.TicketClient.PostAsJsonAsync("https://localhost:7106/api/Tickets/", ticket1);
                response.EnsureSuccessStatusCode();
                string ticket = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket>(ticket);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> PutTicket(int id, Ticket ticket)
        {
            try
            {
                HttpResponseMessage response = await TicketService.TicketClient.PutAsJsonAsync("https://localhost:7106/api/Tickets/" + id, ticket);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> DeleteTicket(int id)
        {
            try
            {
                HttpResponseMessage response = await TicketService.TicketClient.DeleteAsync("https://localhost:7106/api/Tickets/" + id);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
