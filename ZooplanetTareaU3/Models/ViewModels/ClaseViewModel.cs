namespace ZooplanetTareaU3.Models.ViewModels
{
    public class ClaseViewModel
    {
        public int Id { get; set; } 
        public string NombreClase { get; set; } = null!;
        public string Descripcion {  get; set; } = null!;
        public IEnumerable<EspecieClaseModel> Especies {  get; set; }=null!;
        
    }

    public class EspecieClaseModel
    {
        public int Id {  get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class ClasesListaModel
    {
        public IEnumerable<ClaseViewModel> Clases { get; set; } = null!;
    }


}
