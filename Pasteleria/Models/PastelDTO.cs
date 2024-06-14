namespace Pasteleria.Models
{
    public class PastelDTO
    {
        public int id { get; set; }
        public string? Nombre { get; set; }

        public string? Origen { get; set; }

        public decimal? Precio { get; set; }
        public double PromedioSabor { get; set; }
        public double PromedioPresentacion { get; set; }
        public double PromedioFinal { get; set; }
    }
}
