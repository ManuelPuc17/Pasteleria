using Pasteleria.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Pasteleria.Service
{
    public class CalificacionService
    {
        private readonly HttpClient _httpclient;
        private readonly AccesoService _access;

        public CalificacionService(HttpClient httpClient, AccesoService acceso)
        {
            _httpclient = httpClient;
            _access = acceso;
        }

        public async Task<List<CalificacionDTO>> ObtenerCalificacionesAsync(string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.GetAsync("https://localhost:7014/api/Calificacion/Calificaciones");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<CalificacionDTO>>>();
            return result!.Value;
        }

        public async Task<bool> CalificarAsync(CalificacionDTO calificacion, string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.PostAsJsonAsync("https://localhost:7014/api/Calificacion/Calificar", calificacion);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return result!.IsSuccess;
        }

        public async Task<List<CalificacionDTO>> MiCalificacion(int userId, string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.GetAsync($"https://localhost:7014/api/Calificacion/Califi/?userId={userId}");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<CalificacionDTO>>>();
            return result!.Value;
        }      
        
        public async Task<CalificacionDTO> UnaCalificacion(int userId, string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.GetAsync($"https://localhost:7014/api/Calificacion/VistaCalificacion/?id={userId}");
            var result = await response.Content.ReadFromJsonAsync<CalificacionDTO>();
           return result!;

        }

        public async Task<bool> ActualizarCalificacion(CalificacionDTO cali, string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpclient.PostAsJsonAsync("https://localhost:7014/api/Calificacion/EditarCali", cali);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return result!.IsSuccess;
        }

    }

}
