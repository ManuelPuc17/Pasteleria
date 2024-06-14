namespace Pasteleria.Models
{
    public class ResultLogin
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
