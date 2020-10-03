using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI3.Models;
using TestAPI3.ViewModel;

namespace TestAPI3.Repository
{
    public interface IPatientRepository
    {
        Task<List<Patients>> GetPatients();
        Task<Patients> GetPatient(int? Id);
        Task<int> AddPatient(Patients patient);
        Task<int> DeletePatient(int? Id);
        Task UpdatePatient(Patients patient);

    }
}
