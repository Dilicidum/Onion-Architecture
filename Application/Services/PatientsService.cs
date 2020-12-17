using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces.Services;
using Application.Interfaces.Persistence;
using System.Threading.Tasks;
using Application.Models.DTO;
using AutoMapper;
using Domain.Entities;
namespace Application.Services
{
    public class PatientsService:IPatientsService
    {
        IUnitOfWork unitOfWork;
        IMapper mapper;
        public PatientsService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddPatient(PatientDTO patientDTO)
        {
            var patient = mapper.Map<Patient>(patientDTO);
            await unitOfWork.PatientsRepository.InsertAsync(patient);
            await unitOfWork.Commit();
        }

        public async Task ChangePatient(int patientId, PatientDTO patientDTO)
        {
            var patient = mapper.Map<Patient>(patientDTO);
            unitOfWork.PatientsRepository.Update(patient);
            await unitOfWork.Commit();
        }

        public async Task DeletePatient(int PatientId)
        {
            await unitOfWork.PatientsRepository.DeleteByIdAsync(PatientId);
            await unitOfWork.Commit();
        }

        public async Task<IEnumerable<PatientDTO>> GetAllPatients()
        {
            var patients = await unitOfWork.PatientsRepository.GetAsync();
            var patientsDTO = mapper.Map<IEnumerable<PatientDTO>>(patients);
            return patientsDTO;
        }

        public async Task<PatientDTO> GetPatientById(int id)
        {
            var patient = await unitOfWork.PatientsRepository.GetByIdAsync(id);
            var patientDTO = mapper.Map<PatientDTO>(patient);
            return patientDTO;
        }
    }
}
