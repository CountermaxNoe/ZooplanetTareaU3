namespace ZooplanetTareaU3.Areas.Admin.Models
{
    public class EditarAdminEspecieViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Observaciones { get; set; } = null!;
        public int Tamaño { get; set; }
        public double Peso { get; set; }
        public string Habitat { get; set; } = null!;
        public int IdClase { get; set; }
        public IFormFile? Imagen { get; set; }
        public IEnumerable<ClaseAdminModel>? Clases { get; set; }
    }
}
