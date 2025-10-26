namespace ZooplanetTareaU3.Areas.Admin.Models
{
    public class IndexAdminEspeciesViewModel
    {
        public IEnumerable<EspecieAdmin> EspeciesAdmin { get; set; } = null!;
    }

    public class EspecieAdmin
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clase { get; set; } = null!;
    }
    
    public class ClaseAdminModel
    {
        public int Id {  set; get; }
        public string Nombre {  set; get; } = null!;
    }
}
