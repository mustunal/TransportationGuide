using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.Entities;

namespace TransportationGuide.RepositoryLayer.Repos
{
    public interface IRepository<T> : IDisposable where T : class, IEntity
    {
        bool Add(T newEntity);
        bool Delete(T entity);
        bool Update(T entity);
        T FindById(int entityId);
        IEnumerable<T> GetAll();
    }
}
