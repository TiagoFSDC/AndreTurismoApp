using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Net;

namespace AndreTurismoApp.Services
{
    public class PacketService
    {
        static readonly HttpClient packetClient = new HttpClient();
        public async Task<List<Packet>> GetPacket()
        {
            try
            {
                HttpResponseMessage response = await PacketService.packetClient.GetAsync("https://localhost:7164/api/Packets");
                response.EnsureSuccessStatusCode();
                string packet = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Packet>>(packet);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Packet> GetPacketById(int id)
        {
            try
            {
                HttpResponseMessage response = await PacketService.packetClient.GetAsync("https://localhost:7164/api/Packets/" + id);
                response.EnsureSuccessStatusCode();
                string packet = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Packet>(packet);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
        public async Task<Packet> PostPacket(Packet packet1)
        {
            try
            {
                HttpResponseMessage response = await PacketService.packetClient.PostAsJsonAsync("https://localhost:7164/api/Packets/", packet1);
                response.EnsureSuccessStatusCode();
                string packet = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Packet>(packet);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> PutPacket(int id, Packet packet)
        {
            try
            {
                HttpResponseMessage response = await PacketService.packetClient.PutAsJsonAsync("https://localhost:7164/api/Packets/" + id, packet);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> DeletePacket(int id)
        {
            try
            {
                HttpResponseMessage response = await PacketService.packetClient.DeleteAsync("https://localhost:7164/api/Packets/" + id);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
