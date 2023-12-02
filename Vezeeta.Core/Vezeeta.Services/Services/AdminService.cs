using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Repositories;
using Vezeeta.Services.Interfaces;

namespace Vezeeta.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository repository;

        public AdminService(IAdminRepository repository)
        {
            this.repository = repository;
        }
     
        public int NumOfDoctors()
        {
          return  repository.NumOfDoctors();
        }

        public int NumOfPatients()
        {
            return repository.NumOfPatients();
        }
        public dynamic NumOfRequests()
        { return repository.NumOfRequests(); }
        public dynamic Top10Doctors()
        {return repository.Top10Doctors();
        }

        public dynamic Top5Specializations()
        {
            return repository.Top5Specializations();
        }
    }
}
