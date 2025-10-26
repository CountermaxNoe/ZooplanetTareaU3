using Microsoft.EntityFrameworkCore;
using ZooplanetTareaU3.Models.Entities;

namespace ZooplanetTareaU3.Repositories
{
    public class Repository<T> where T : class
    {
        public Repository(AnimalesContext context) 
        {
            Context = context;
        }
        public AnimalesContext Context { get; }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public T? Get(object id)
        {
            var entityType = typeof(T);

            if (entityType == typeof(Especies))
            {
                return Context.Set<Especies>()
                    .Include(e => e.IdClaseNavigation)
                    .FirstOrDefault(e => e.Id == (int)id) as T;
            }

            return Context.Find<T>(id);
        }


        public void Insert(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public void Delete(object id)
        {
            T? entity = Get(id);
            if (entity != null)
            {
                Context.Remove(entity);
                Context.SaveChanges();
            }
        }
    }
}
