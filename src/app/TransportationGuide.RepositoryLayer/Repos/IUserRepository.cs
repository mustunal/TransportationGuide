using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.Entities;

namespace TransportationGuide.RepositoryLayer.Repos
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUserName(string userName);
    }
}
