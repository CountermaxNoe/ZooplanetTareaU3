using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Core.Types;
using ZooplanetTareaU3.Areas.Admin.Models;
using ZooplanetTareaU3.Models.Entities;
using ZooplanetTareaU3.Models.ViewModels;
using ZooplanetTareaU3.Repositories;

namespace ZooplanetTareaU3.Services
{
    public class EspeciesServices
    {
        public EspeciesServices(Repository<Especies> especiesRepository, Repository<Clase> clasesRepository, IWebHostEnvironment hostEnvironment)
        {
            EspeciesRepository=especiesRepository;
            ClaseRepository=clasesRepository;
            HostEnvironment= hostEnvironment;

        }
        public Repository<Clase> ClaseRepository { get; }
        public Repository<Especies> EspeciesRepository { get; }
        public IWebHostEnvironment HostEnvironment { get; }


        public ClaseViewModel GetEspeciesByClase(string nombre)
        {
            ClaseViewModel vm = new();
            vm.NombreClase = nombre;
            vm.Especies= EspeciesRepository.GetAll().AsQueryable().
                Where(x=>x.IdClaseNavigation!=null && x.IdClaseNavigation.Nombre==nombre).
                OrderBy(x=>x.Especie).Select(x=> new EspecieClaseModel
                {
                    Id=x.Id,
                    Nombre=x.Especie??""
                });

            return vm;
        }

        public EspecieViewModel? GetEspecie(string nombre)
        {
            EspecieViewModel? vm = EspeciesRepository.GetAll().AsQueryable()
                .Include(x => x.IdClaseNavigation).Where(x => x.Especie == nombre)
                .Select(x => new EspecieViewModel
                {
                    Id = x.Id,
                    Habitat = x.Habitat??"",
                    Nombre = x.Especie??"",
                    Observaciones = x.Observaciones??"",
                    Peso=x.Peso,
                    Tamaño = x.Tamaño,
                    Clase = x.IdClaseNavigation != null ? x.IdClaseNavigation.Nombre ?? "" : ""
                }).FirstOrDefault();

            return vm;
        }

        public IndexAdminEspeciesViewModel GetEspeciesAdministrador()
        {
            var especies = EspeciesRepository.GetAll().AsQueryable().Include(x=>x.IdClaseNavigation).OrderBy(x => x.Especie)
                .Select(x => new EspecieAdmin
                {
                    Nombre = x.Especie,
                    Clase = x.IdClaseNavigation.Nombre??"",
                    Id = x.Id
                }).ToList();

            var vm = new IndexAdminEspeciesViewModel
            {
                EspeciesAdmin = especies
            };

            return vm;
        }

        public AgregarAdminEspecieViewModel GetForAgregar()
        {
            return new AgregarAdminEspecieViewModel
            {
                Clases = ClaseRepository.GetAll().Select(x => new ClaseAdminModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre ?? "--Sin Nombre--"
                })
            };
        }

        public void Agregar(AgregarAdminEspecieViewModel vm)
        {
            var entidad = new Especies
            {
                Id = 0,
                Especie = vm.Nombre,
                Habitat = vm.Habitat,
                Tamaño = vm.Tamaño,
                Peso = vm.Peso,
                Observaciones = vm.Observaciones,
                IdClase = vm.IdClase
            };

            EspeciesRepository.Insert(entidad);

            var id = entidad.Id;

            if (vm.Imagen != null)
            {
                AgregarImagen(vm.Imagen, id);
            }
            else
            {
                UsarImagenPorDefecto(id);
            }
        }

        public void AgregarImagen(IFormFile archivo, int IdEspecie)
        {
            if (archivo.Length > 1024 * 1024 * 2)
            {
                throw new ArgumentException("Seleccione una imagen de 2MB o menos.");
            }

            if (archivo.ContentType != "image/jpeg") 
            {
                throw new ArgumentException("Selecciones una imagen JPEG o JPG");
            }


            var ruta = HostEnvironment.WebRootPath + $"/especies/{IdEspecie}.jpg";
            var file = File.Create(ruta);
            archivo.CopyTo(file);
            file.Close();
        }

        private void UsarImagenPorDefecto(int IdEspecie)
        {
            var rutaDefecto = Path.Combine(HostEnvironment.WebRootPath, "especies", "nophoto.jpg");
            var rutaDestino = Path.Combine(HostEnvironment.WebRootPath, "especies", $"{IdEspecie}.jpg");
            Directory.CreateDirectory(Path.GetDirectoryName(rutaDestino)!);

            if (File.Exists(rutaDefecto))
            {
                File.Copy(rutaDefecto, rutaDestino, true);
            }
        }

        public EliminarAdminViewModel GetForEliminar(int id)
        {
            var entidad = EspeciesRepository.Get(id);
            if (entidad == null) throw new ArgumentException("Especie no encontrada.", nameof(id));

            return new EliminarAdminViewModel
            {
                Id = entidad.Id,
                Nombre = entidad.Especie ?? "--Sin Nombre--",
                Clase=entidad.IdClaseNavigation.Nombre??""
            };
        }

        public void Eliminar(int id)
        {
            var entidad = EspeciesRepository.Get(id);
            if (entidad == null) throw new ArgumentException("Especie no encontrada.");

            EspeciesRepository.Delete(entidad.Id);

            var rutaImagen = Path.Combine(HostEnvironment.WebRootPath, "especies", $"{id}.jpg");
            if (File.Exists(rutaImagen))
                File.Delete(rutaImagen);
        }

        public EditarAdminEspecieViewModel GetForEditar(int id)
        {
            var entidad = EspeciesRepository.Get(id);
            if (entidad == null) throw new ArgumentException("Especie no encotrada.");

            return new EditarAdminEspecieViewModel
            {
                Id = entidad.Id,
                Nombre = entidad.Especie ?? "--Sin Nombre--",
                IdClase = entidad.IdClase,
                Habitat = entidad.Habitat ?? "",
                Peso = entidad.Peso,
                Observaciones = entidad.Observaciones ?? "",
                Clases = ClaseRepository.GetAll()
                    .Select(c => new ClaseAdminModel { Id = c.Id, Nombre = c.Nombre ?? "--Sin Nombre--" })
            };
        }

        public void Editar(EditarAdminEspecieViewModel vm)
        {
            var entidad= EspeciesRepository.Get(vm.Id);
            if (entidad == null) throw new ArgumentException("Especie no encontrada.");

            entidad.Especie = vm.Nombre;
            entidad.IdClase = vm.IdClase;
            entidad.Observaciones = vm.Observaciones;
            entidad.Habitat = vm.Habitat;
            entidad.Peso = vm.Peso;
            entidad.Tamaño = vm.Tamaño;

            EspeciesRepository.Update(entidad);

            if (vm.Imagen != null)
            {
                AgregarImagen(vm.Imagen, entidad.Id);
            }


        }
    }
}
