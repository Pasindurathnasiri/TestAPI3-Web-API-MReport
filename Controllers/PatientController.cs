using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAPI3.Models;
using TestAPI3.Repository;

namespace TestAPI3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IPatientRepository patientRepository;
        public PatientController(IPatientRepository _patientRepository)
        {
            patientRepository = _patientRepository;
        }

        [HttpGet]
        [Route("GetPatient")]
        public async Task<IActionResult> GetPatients()
        {
            try
            {
                var patients = await patientRepository.GetPatients();
                if (patients == null)
                {
                    return NotFound();
                }

                return Ok(patients);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetPatientById")]
        public async Task<IActionResult> GetPatient(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            try
            {
                var patient = await patientRepository.GetPatient(Id);

                if (patient == null)
                {
                    return NotFound();
                }

                return Ok(patient);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddPatient")]
        public async Task<IActionResult> AddPatient([FromBody] Patients model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Id = await patientRepository.AddPatient(model);
                    if (Id > 0)
                    {
                        return Ok(Id);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }


        [HttpDelete]
        [Route("DeletePatient")]
        public async Task<IActionResult> DeletePatient(int? Id)
        {
            int result = 0;

            if (Id == null)
            {
                return BadRequest();
            }

            try
            {
                result = await patientRepository.DeletePatient(Id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut]
        [Route("UpdatePatient")]
        public async Task<IActionResult> UpdatePatient([FromBody] Patients model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await patientRepository.UpdatePatient(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().Name ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }



    }
}
