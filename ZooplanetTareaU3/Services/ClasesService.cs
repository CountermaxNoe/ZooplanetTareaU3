using NuGet.Protocol.Core.Types;
using ZooplanetTareaU3.Models.Entities;
using ZooplanetTareaU3.Models.ViewModels;
using ZooplanetTareaU3.Repositories;

namespace ZooplanetTareaU3.Services
{
    public class ClasesService
    {
        public ClasesService(Repository<Clase> claseRepository)
        {
            ClaseRepository=claseRepository;
        }
        public Repository<Clase> ClaseRepository { get; }

        public IEnumerable<string> GetNombreClases()
        {
            return ClaseRepository.GetAll().Select(x => x.Nombre ?? "").OrderBy(x => x);
        }

        public ClasesListaModel GetClasesDescripcion()
        {
            var clases = ClaseRepository.GetAll()
                .OrderBy(x => x.Nombre)
                .Select(x => new ClaseViewModel
                {
                    Id = x.Id,
                    NombreClase = x.Nombre ?? "",
                    Descripcion = x.Descripcion ?? ""
                })
                .ToList();

            var vm = new ClasesListaModel
            {
                Clases = clases
            };

            return vm;
        }
    }
}
