using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.Entities;
using TransportationGuide.RepositoryLayer.Repos;

namespace TransportationGuide.RepositoryLayer.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        readonly string ConnectionString;
        readonly DbContext _context = null;
        UserRepository _userRepository = null;

        public UnitOfWork()
        {
            //ConnectionStringi Configden al
            ConnectionString = "ConnectionString";
            _context = new DbContext(ConnectionString);
            _context.Configuration.LazyLoadingEnabled = true;
        }

        public IRepository<User> EmployeeRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);

                return _userRepository;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void RollBack()
        {

        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
            if (_userRepository != null)
            {
                _userRepository.Dispose();
                _userRepository = null;
            }
        }


    }
}
