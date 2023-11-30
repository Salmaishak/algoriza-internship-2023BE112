using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Infrastructure.DbContexts;

namespace Vezeeta.Infrastructure.RepositoriesImplementation
{
    public  class AdminRepository : IAdminRepository
    {
        private readonly VezeetaContext _context;
        public AdminRepository(VezeetaContext context) { _context = context; }
    

        public int NumOfDoctors()
        {
            return _context.Doctors.Count<Doctor>();
        }

        public int NumOfPatients()
        {
            return _context.AllUsers.Count<User>(d=>d.type == UserType.patient);
        }

        public dynamic Top10Doctors()
        {
            throw new NotImplementedException();
        }

        public dynamic Top5Specializations()
        {
            throw new NotImplementedException();
        }
    }
}
