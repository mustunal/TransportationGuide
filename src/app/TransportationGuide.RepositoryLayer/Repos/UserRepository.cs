using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.Entities;

namespace TransportationGuide.RepositoryLayer.Repos
{
    public class UserRepository : IRepository<User>
    {
        DbContext _context = null;

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public bool Add(User newEntity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User FindById(int entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
