using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Models.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        IPatientsService patientsService;
        public PatientsController(IPatientsService patientsService)
        {
            this.patientsService = patientsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetAllPatients()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(c => c.Errors);
                return BadRequest($"Error occured = {errors}");
            }
            var patiens = await patientsService.GetAllPatients();
            return patiens.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatientById(int id)
        {
            var patient = await patientsService.GetPatientById(id);
            return patient;
        }

        [HttpPost]
        public async Task<ActionResult> AddPatient([FromBody] PatientDTO patient)
        {
            await patientsService.AddPatient(patient);
            return CreatedAtAction(nameof(GetPatientById), new { Id = 1 }, patient);
        }

        [HttpPut("{patientId}")]
        public async Task<ActionResult> ChangePatient(int patientId, PatientDTO patient)
        {
            await patientsService.ChangePatient(patientId, patient);
            return NoContent();
        }

        [HttpDelete("{patientId}")]
        public async Task<ActionResult> DeletePatientById(int patientId)
        {
            await patientsService.DeletePatient(patientId);
            return Ok();
        }

        
        

        
    }
}
