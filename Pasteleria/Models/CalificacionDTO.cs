namespace Pasteleria.Models
{
    public class CalificacionDTO
    {
        public CalificacionDTO()
        {
            ListaPasteles = new List<PastelDTO>(); 
        }
        public int Id { get; set; }

        public string? Usuario { get; set; }
        public int? IdUsuario { get; set; }

        public string? Pastel { get; set; }
        public int? IdPastel { get; set; }

        public double? Sabor { get; set; }

        public double? Presentacion { get; set; }

        public List<PastelDTO> ListaPasteles { get; set; }
    } 
}
