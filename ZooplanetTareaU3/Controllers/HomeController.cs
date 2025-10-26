using Microsoft.AspNetCore.Mvc;
using ZooplanetTareaU3.Models.Entities;
using ZooplanetTareaU3.Models.ViewModels;
using ZooplanetTareaU3.Services;

namespace ZooplanetTareaU3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClasesService clasesService;
        private readonly EspeciesServices especiesService;

        public HomeController(ClasesService clasesService, EspeciesServices especiesServices)
        {
            this.clasesService = clasesService;
            this.especiesService = especiesServices;
        }

        public IActionResult Index()
        {
            ClasesListaModel vm = clasesService.GetClasesDescripcion();
            return View(vm);
        }

        public IActionResult Clase(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var vm = especiesService.GetEspeciesByClase(id);

            return View(vm);
        }

        public IActionResult Detalle(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var vm = especiesService.GetEspecie(id);

            return View(vm);
        }
    }
}
