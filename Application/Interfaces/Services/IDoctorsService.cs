using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.DTO;

namespace Application.Interfaces.Services
{
    public interface IDoctorsService
    {
        Task<IEnumerable<DoctorDTO>> GetAllDoctors();
        Task<DoctorDTO> GetDoctorById(int id);
        Task AddDoctor(DoctorDTO doctor);
        Task ChangeDoctor(int doctorId,DoctorDTO doctor);
        Task DeleteAll();
        Task DeleteById(int id);
        Task<IEnumerable<DoctorDTO>> GetFreeDoctorsAtDate(DateTime date,string specialiation);
        Task AddDoctorSchedule(int doctorId,Doctor_ScheduleDTO doctor_ScheduleDTO);
        Task<IEnumerable<Doctor_ScheduleDTO>> GetDoctorSchedule(int doctorId);
        Task AddAppointment(Patient_VisitingDTO patient_VisitingDTO);
    }
}
