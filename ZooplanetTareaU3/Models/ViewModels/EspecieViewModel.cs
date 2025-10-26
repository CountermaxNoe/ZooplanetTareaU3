namespace ZooplanetTareaU3.Models.ViewModels
{
    public class EspecieViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Observaciones { get; set; } = null!;
        public int Tamaño { get; set; }
        public double Peso { get; set; }
        public string Habitat { get; set; } = null!;
        public string Clase { get; set; } = null!;
    }
}
