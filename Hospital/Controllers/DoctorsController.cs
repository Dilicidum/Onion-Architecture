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
    public class DoctorsController : ControllerBase
    {
        private IDoctorsService doctorsService;
        private IAppointmentsService appointmentsService;
        public DoctorsController(IDoctorsService doctorsService,IAppointmentsService appointmentsService)
        {
            this.appointmentsService = appointmentsService;
            this.doctorsService = doctorsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAllDoctors()
        {
            var doctors = (await doctorsService.GetAllDoctors()).ToList();
            if (doctors.Any())
            {
                return doctors;
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctorById(int id)
        {
            var doctor = await doctorsService.GetDoctorById(id);
            if (doctor != null)
            {
                return doctor;
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpGet("FreeDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetFreeDoctorsAtDate([FromQuery] DateTime date,[FromQuery]string specialization)
        {
            var doctors = await doctorsService.GetFreeDoctorsAtDate(date,specialization);
            if (doctors.Any())
            {
                return doctors.ToList();
            }
            else
            {
                return BadRequest();
            }
            
        }

        

        [HttpPost]
        public async Task<ActionResult> AddDoctor([FromBody] DoctorDTO doctor)
        {
            await doctorsService.AddDoctor(doctor);
            return CreatedAtAction(nameof(GetDoctorById), new { Id = doctor.Id }, doctor);
        }

        [HttpPost("{Id}/Schedule")]
        public async Task<ActionResult> AddDoctorSchedule(int Id, [FromBody] Doctor_ScheduleDTO Doctor_ScheduleDTO)
        {
            await doctorsService.AddDoctorSchedule(Id, Doctor_ScheduleDTO);
            return CreatedAtAction(nameof(GetDoctorSchedule_ByDoctorId), new { Id = Doctor_ScheduleDTO.DoctorId });
        }

        [HttpGet("doctorId/Schedule")]
        public async Task<ActionResult<IEnumerable<Doctor_ScheduleDTO>>> GetDoctorSchedule_ByDoctorId(int doctorId)
        {
            var doctprse_Schedules = await doctorsService.GetDoctorSchedule(doctorId);
            if (doctprse_Schedules.Any())
            {
                return doctprse_Schedules.ToList();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{doctorId}")]
        public async Task<ActionResult<DoctorDTO>> ChangeDoctor(int doctorId, [FromBody] DoctorDTO doctor)
        {
            await doctorsService.ChangeDoctor(doctorId, doctor);
            return Ok(doctor);
        }

        [HttpDelete]
        public async Task<ActionResult<DoctorDTO>> DeleteAllDoctors()
        {
            await doctorsService.DeleteAll();
            return NoContent();
        }

        [HttpGet("{doctorId}/Appointments/{id}")]
        public async Task<ActionResult<Patient_VisitingDTO>> GetAppointmentById(int doctorId, int id)
        {
            var appointment = await appointmentsService.GetAppointment_ByAppointmentId(id);
            if(appointment != null)
            {
                return appointment;
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{doctorId}/Appointments/{id}")]
        public async Task<ActionResult> ChangeAppointmentById(int doctorId, Patient_VisitingDTO patient_VisitingDTO)
        {
            await appointmentsService.ChangeAppointment(patient_VisitingDTO);
            return NoContent();
        }

        [HttpPost("{doctorId}/Appointments")]
        public async Task<ActionResult> AddAppointment([FromRoute]int doctorId,[FromBody] Patient_VisitingDTO patient_VisitingDTO)
        {
            patient_VisitingDTO.DoctorId = doctorId;
            var x = await appointmentsService.AddAppointment(patient_VisitingDTO);
            return CreatedAtAction(nameof(GetAppointmentById),new { Id = x.AppointmentId});
        }

        [HttpGet("{doctorId}/Appointments")]
        public async Task<ActionResult<IEnumerable<Patient_VisitingDTO>>> GetAllCurrentAppointments(int doctorId)
        {
            var appointments = await appointmentsService.GetCurrentAppointments_ByDoctorId(doctorId);
            if (appointments.Any())
            {
                return appointments.ToList();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete("{doctorId}/Appointments")]
        public async Task<ActionResult> DeleteAllAppointments(int doctorId)
        {
            await appointmentsService.RemoveAllAppointments_ByDoctorId(doctorId);
            return NoContent();
        }

        [HttpGet("{doctorId}/Appointments/Today")]
        public async Task<ActionResult<IEnumerable<Patient_VisitingDTO>>> GetAppointmentsForToday(int doctorId)
        {
            
            var appointments = await appointmentsService.GetAppointments_ByDoctorId_ForToday(doctorId);
            if (appointments.Any())
            {
                return appointments.ToList();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{doctorId}/Appointments/Past")]
        public async Task<ActionResult<IEnumerable<Patient_VisitingDTO>>> GetPastAppointments(int doctorId)
        {
            
            var appointments = await appointmentsService.GetPastAppointments_ByDoctorId(doctorId);
            if (appointments.Any())
            {
                return appointments.ToList();
            }
            else
            {
                return BadRequest();
            }
            

        }

        [HttpGet("{doctorId}/Appointments/Next")]
        public async Task<ActionResult<Patient_VisitingDTO>> GetNextAppointment(int doctorId)
        {
            var appointment = await appointmentsService.GetNextAppointment_ByDoctorId(doctorId);
            return appointment;
        }

        [HttpDelete]
        public async Task<ActionResult<DoctorDTO>> DeleteDoctorById(int doctorId, [FromBody] DoctorDTO doctor)
        {
            await doctorsService.DeleteById(doctorId);
            return Ok();
        }

    }
}
