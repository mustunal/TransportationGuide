using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.Entities;

namespace TransportationGuide.DataAccessLayer
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext()
            : base("TransportationGuideConnectionString")
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
