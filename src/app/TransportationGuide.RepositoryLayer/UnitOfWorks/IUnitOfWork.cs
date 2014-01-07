using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.Entities;
using TransportationGuide.RepositoryLayer.Repos;

namespace TransportationGuide.RepositoryLayer.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IRepository<User> EmployeeRepository { get; }

        void Commit();
        void RollBack();
    }
}
