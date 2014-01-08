using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.DataAccessLayer;
using TransportationGuide.DataAccessLayer.Migrations;
using TransportationGuide.Entities;
using TransportationGuide.RepositoryLayer.Repos;

namespace TransportationGuide.RepositoryLayer.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly string ConnectionString;
        readonly EntitiesContext _context = null;

        IUserRepository _userRepository = null;

        public UnitOfWork()
        {
            //ConnectionStringi Configden al
            ConnectionString = "TransportationGuideConnectionString";
            _context = new EntitiesContext(ConnectionString);
            _context.Configuration.LazyLoadingEnabled = true;
            InitializeDataBase();
        }

        private void InitializeDataBase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntitiesContext, Configuration>());
            _context.Database.Initialize(true);
        }

        public IUserRepository UserRepository
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
