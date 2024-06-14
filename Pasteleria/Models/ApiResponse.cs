using Newtonsoft.Json;

namespace Pasteleria.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Token { get; set; }
        public T Value { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }


    }
}
