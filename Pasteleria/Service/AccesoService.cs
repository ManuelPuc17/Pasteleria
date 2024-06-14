using Pasteleria.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace Pasteleria.Service
{
    public class AccesoService
    {
        private readonly HttpClient _httpclient;

        public AccesoService( HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<bool>RegistrarseAsync(UsuarioDTO user)
        {
            var response = await _httpclient.PostAsJsonAsync("https://localhost:7014/api/Acceso/Registarse", user);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return result!.IsSuccess;
        }

        public async Task<ResultLogin> LoginAsync(LoginDTO model)
        {
            var response = await _httpclient.PostAsJsonAsync("https://localhost:7014/api/Acceso/Login", model);
            var loginResponse = await response.Content.ReadFromJsonAsync<ResultLogin>();

            return loginResponse;
        }

    }
}
