using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Pasteleria.Models;


namespace Pasteleria.Service
{
    public class PastelService
    {
        private readonly HttpClient _httpclient;


        public PastelService(HttpClient httpClient)
        {
            _httpclient = httpClient;
            

        }

        public async Task<List<PastelDTO>> ObtenerListaAsync(string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.GetAsync("https://localhost:7014/api/Pastel/Lista");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<PastelDTO>>>();
            return result!.Value;
        }

        public async Task<PastelDTO> ObtenerListaidAsync(int id, string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.GetAsync($"https://localhost:7014/api/Pastel/Obtener?id={id}");
            var result = await response.Content.ReadFromJsonAsync<PastelDTO>();
            return result!;
        }


        public async Task<bool> AgregarAsync(PastelDTO pastel, string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.PostAsJsonAsync("https://localhost:7014/api/Pastel/Agregar", pastel);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return result!.IsSuccess;
        }

        public async Task<bool> EditarAsync(PastelDTO pastel, string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.PostAsJsonAsync("https://localhost:7014/api/Pastel/Editar", pastel);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return result!.IsSuccess;
        }

        public async Task<bool> EliminarAsync(int id, string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.DeleteAsync($"https://localhost:7014/api/Pastel/Eliminar/{id}");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return result!.IsSuccess;
        }
    }
}
