using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportationGuide.RepositoryLayer.UnitOfWorks;

namespace TransportationGuide.BusinessLogicLayer
{
    public class CommonBL
    {
        public static void InitializeDataBase()
        {
            using (var uow = new UnitOfWork())
            {
                uow.InitializeDataBase();
            }
        }
    }
}
