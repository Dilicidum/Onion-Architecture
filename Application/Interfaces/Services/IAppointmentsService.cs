using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.DTO;

namespace Application.Interfaces.Services
{
    public interface IAppointmentsService
    {
        //ADD AND CHANGE
        Task<Patient_VisitingDTO> AddAppointment(Patient_VisitingDTO patient_VisitingDTO);
        Task<Patient_VisitingDTO> ChangeAppointment(Patient_VisitingDTO patient_VisitingDTO);

        //GET
        Task<IEnumerable<Patient_VisitingDTO>> GetAllCurrentAppointments();
        Task<Patient_VisitingDTO> GetAppointment_ByAppointmentId(int appointmentId);
        Task<IEnumerable<Patient_VisitingDTO>> GetCurrentAppointments_ByDoctorId(int doctorId);
        Task<IEnumerable<Patient_VisitingDTO>> GetPastAppointments_ByDoctorId(int doctoId);
        Task<IEnumerable<Patient_VisitingDTO>> GetAppointments_ByDoctorId_ForToday(int doctoId);
        Task<Patient_VisitingDTO> GetNextAppointment_ByDoctorId(int doctorId);
        //Remove
        Task RemoveAppointment_ByAppointmentId(int appointmentId);
        
        
    }
}
