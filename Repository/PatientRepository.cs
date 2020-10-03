using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI3.Models;
using TestAPI3.ViewModel;

namespace TestAPI3.Repository
{
    public class PatientRepository : IPatientRepository
    {
        TestMReportContext db;
        public PatientRepository(TestMReportContext _db)
        {
            db = _db;
        }
        public async Task<int> AddPatient(Patients patient)
        {
            if (db != null)
            {
                await db.Patients.AddAsync(patient);
                await db.SaveChangesAsync();

                return patient.Id;
            }

            return 0;
        }

        public async Task<int> DeletePatient(int? Id)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var patient = await db.Patients.FirstOrDefaultAsync(x => x.Id == Id);

                if (patient != null)
                {
                    //Delete that post
                    db.Patients.Remove(patient);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<Patients> GetPatient(int? Id)
        {
            if (db != null)
            {
                return await(from p in db.Patients
                             
                             where p.Id == Id
                             select new Patients
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Age = p.Age,
                                 Address = p.Address


                             }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<Patients>> GetPatients()
        {
            if (db != null)
            {
                return await db.Patients.ToListAsync();
            }

            return null;
        }

        public async Task UpdatePatient(Patients patient)
        {
            if (db != null)
            {
                //Delete that post
                db.Patients.Update(patient);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }
}
