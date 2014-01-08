using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.DataAccessLayer;
using TransportationGuide.Entities;

namespace TransportationGuide.RepositoryLayer.Repos
{
    public class UserRepository : IUserRepository
    {
        readonly EntitiesContext _context = null;

        public UserRepository(EntitiesContext context)
        {
            _context = context;
        }

        public bool Add(User newEntity)
        {
            _context.Users.Add(newEntity);
            return true;
        }

        public bool Delete(User entity)
        {
            _context.Users.Remove(entity);
            return true;
        }

        public bool Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return true;
        }

        public User FindById(int entityId)
        {
            return _context.Users.SingleOrDefault(x => x.Id == entityId);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(x => x.Username.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
