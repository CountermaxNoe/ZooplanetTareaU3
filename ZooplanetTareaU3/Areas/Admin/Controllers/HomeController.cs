using Microsoft.AspNetCore.Mvc;
using ZooplanetTareaU3.Areas.Admin.Models;
using ZooplanetTareaU3.Services;

namespace ZooplanetTareaU3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly EspeciesServices especiesService;

        public HomeController(EspeciesServices especiesServices)
        {
            this.especiesService = especiesServices;
        }
        public IActionResult Index()
        {
            var vm = especiesService.GetEspeciesAdministrador();
            return View(vm);
        }
        [HttpGet]
        public IActionResult Agregar()
        {
            var vm = especiesService.GetForAgregar();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(AgregarAdminEspecieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Clases=especiesService.GetForAgregar().Clases;
                return View(vm);
            }

            especiesService.Agregar(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var vm = especiesService.GetForEliminar(id);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Eliminar(EliminarAdminViewModel vm)
        {
            especiesService.Eliminar(vm.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var vm = especiesService.GetForEditar(id);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Editar(EditarAdminEspecieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Clases = especiesService.GetForAgregar().Clases;
                return View(vm);
            }

            especiesService.Editar(vm);
            return RedirectToAction("Index");
        }

    }
}
