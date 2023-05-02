using AndreTurismoApp.Models;
using Newtonsoft.Json;
using System.Net;

namespace AndreTurismoApp.Services
{
    public class HotelService
    {
        static readonly HttpClient hotelClient = new HttpClient();
        public async Task<List<Hotel>> GetHotel()
        {
            try
            {
                HttpResponseMessage response = await HotelService.hotelClient.GetAsync("https://localhost:7241/api/Hotels");
                response.EnsureSuccessStatusCode();
                string hotel = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Hotel>>(hotel);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            try
            {
                HttpResponseMessage response = await HotelService.hotelClient.GetAsync("https://localhost:7241/api/Hotels/" + id);
                response.EnsureSuccessStatusCode();
                string hotel = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Hotel>(hotel);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
        public async Task<Hotel> PostHotel(Hotel h)
        {
            try
            {
                HttpResponseMessage response = await HotelService.hotelClient.PostAsJsonAsync("https://localhost:7241/api/Hotels/", h);
                response.EnsureSuccessStatusCode();
                string hotel = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Hotel>(hotel);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> PutHotel(int id, Hotel hotel)
        {
            try
            {
                HttpResponseMessage response = await HotelService.hotelClient.PutAsJsonAsync("https://localhost:7241/api/Hotels/" + id, hotel);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> DeleteHotel(int id)
        {
            try
            {
                HttpResponseMessage response = await HotelService.hotelClient.DeleteAsync("https://localhost:7241/api/Hotels/" + id);
                return response.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
