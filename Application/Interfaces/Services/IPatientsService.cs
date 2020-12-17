using System;
using System.Collections.Generic;
using System.Text;
using Application.Models.DTO;
using System.Threading.Tasks;
namespace Application.Interfaces.Services
{
    public interface IPatientsService
    {
        Task<IEnumerable<PatientDTO>> GetAllPatients();
        Task<PatientDTO> GetPatientById(int id);
        Task AddPatient(PatientDTO patientDTO);
        Task ChangePatient(int patientId,PatientDTO patientDTO);
        Task DeletePatient(int PatientId);
        
    }
}
