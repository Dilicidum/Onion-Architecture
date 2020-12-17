using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Models.DTO;
using Application.Interfaces.Services;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController:ControllerBase
    {
        private IAppointmentsService appointmentsService;
        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            this.appointmentsService = appointmentsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient_VisitingDTO>>> GetAllCurrentAppointments()
        {
            var appointments = await appointmentsService.GetAllCurrentAppointments();
            return appointments.ToList();
        }

        [HttpGet("{appointmentId}")]
        public async Task<ActionResult<Patient_VisitingDTO>> GetAppointmentById(int appointmentId)
        {
            var appointment = await appointmentsService.GetAppointment_ByAppointmentId(appointmentId);
            return appointment;
        }

        [HttpGet("Current")]
        public async Task<ActionResult<IEnumerable<Patient_VisitingDTO>>> GetCurrentAppointments()
        {
            var appointments = await appointmentsService.GetAllCurrentAppointments();
            return appointments.ToList();
        }

        [HttpPut("{appointmentId}")]
        public async Task<ActionResult> ChangeAppointmentById(Patient_VisitingDTO patient_VisitingDTO)
        {
            await appointmentsService.ChangeAppointment(patient_VisitingDTO);
            return NoContent();
        }
            
    }
}
