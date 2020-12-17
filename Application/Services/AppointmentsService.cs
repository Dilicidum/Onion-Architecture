using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Application.Models.DTO;
using System.Linq;
using Application.Interfaces.Persistence;
using Domain.Entities;
using AutoMapper;
namespace Application.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        public AppointmentsService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Patient_VisitingDTO> AddAppointment(Patient_VisitingDTO patient_VisitingDTO)
        {
            var schedule = (await unitOfWork.Doctors_SheduleRepository
                .GetAsync(doc_shd => doc_shd.DoctorId == patient_VisitingDTO.DoctorId
                && doc_shd.DayOfWeek == patient_VisitingDTO.TimeOfVisit.DayOfWeek)).FirstOrDefault();
            var doctor = (await unitOfWork.DoctorsRepository
                .GetAsync(doc=>doc.Id == patient_VisitingDTO.DoctorId, null, "Doctor_Schedules,Appointments")).FirstOrDefault();

            Patient_Visiting patient_Visiting = mapper.Map<Patient_Visiting>(patient_VisitingDTO);
            TimeSpan startTime = schedule.StartTime;
            TimeSpan temp = doctor.TimeToTakePatient;

            while (startTime + temp <= schedule.EndTime)
            {
                if(startTime == schedule.BreakTimeStart)
                {
                    startTime = schedule.BreakEndTime;
                }

                if(startTime == patient_VisitingDTO.TimeOfVisit.TimeOfDay)
                {
                    var appointmentOnThatTime = (await unitOfWork.Patients_VisitingsRepository
                    .GetAsync(app =>
                    (app.TimeOfVisit.TimeOfDay == startTime)
                    &&
                    app.DoctorId == patient_VisitingDTO.DoctorId
                    ))
                    .FirstOrDefault();
                    if (appointmentOnThatTime == null)
                    {
                        await unitOfWork.Patients_VisitingsRepository.InsertAsync(patient_Visiting);
                        await unitOfWork.Commit();
                    }
                }

                startTime += temp;

            }
            
            patient_VisitingDTO.AppointmentId = patient_Visiting.AppointmentId;
            return patient_VisitingDTO;
        }

        public async Task<Patient_VisitingDTO> ChangeAppointment(Patient_VisitingDTO patient_VisitingDTO)
        {
            var appointment = await unitOfWork.Patients_VisitingsRepository.GetByIdAsync(patient_VisitingDTO.AppointmentId);
            mapper.Map(patient_VisitingDTO,appointment);
            await unitOfWork.Commit();
            return patient_VisitingDTO;
        }

        public async Task<IEnumerable<Patient_VisitingDTO>> GetAllCurrentAppointments()
        {
            var appointments = await unitOfWork.Patients_VisitingsRepository.GetAsync(c => c.TimeOfVisit >= DateTime.Now);
            var appointmentsDTO = mapper.Map<IEnumerable<Patient_VisitingDTO>>(appointments);
            return appointmentsDTO;
        }

        public async Task<IEnumerable<Patient_VisitingDTO>> GetAppointments_ByDoctorId_ForToday(int doctoId)
        {
            var appointments = await unitOfWork.Patients_VisitingsRepository
                .GetAsync(c => c.TimeOfVisit == DateTime.Today && c.TimeOfVisit >= DateTime.Now);
            var appointmentsDTO = mapper.Map<IEnumerable<Patient_VisitingDTO>>(appointments);
            return appointmentsDTO;
        }

        public async Task<Patient_VisitingDTO> GetAppointment_ByAppointmentId(int appointmentId)
        {
            var appointment = await unitOfWork.Patients_VisitingsRepository.GetByIdAsync(appointmentId);
            var appointmentDTO = mapper.Map<Patient_VisitingDTO>(appointment);
            return appointmentDTO;
        }

        public async Task<IEnumerable<Patient_VisitingDTO>> GetCurrentAppointments_ByDoctorId(int doctorId)
        {
            var appointments = await unitOfWork.Patients_VisitingsRepository
                .GetAsync(app => app.TimeOfVisit >= DateTime.Now && app.DoctorId == doctorId);
            var appointmentsDTO = mapper.Map<IEnumerable<Patient_VisitingDTO>>(appointments);
            return appointmentsDTO;
        }

        public async Task<Patient_VisitingDTO> GetNextAppointment_ByDoctorId(int doctorId)
        {
            var appointment = (await unitOfWork.Patients_VisitingsRepository
                .GetAsync(c => c.TimeOfVisit == DateTime.Today && c.TimeOfVisit >= DateTime.Now && c.DoctorId == doctorId)).FirstOrDefault();
            var appointmentDTO = mapper.Map<Patient_VisitingDTO>(appointment);
            return appointmentDTO;
        }

        public async Task<IEnumerable<Patient_VisitingDTO>> GetPastAppointments_ByDoctorId(int doctoId)
        {
            var appointments = await unitOfWork.Patients_VisitingsRepository
                .GetAsync(app => app.DoctorId == doctoId && app.TimeOfVisit <= DateTime.Now);
            var appointmentsDTO = mapper.Map<IEnumerable<Patient_VisitingDTO>>(appointments);
            return appointmentsDTO;
        }

        

        public async Task RemoveAppointment_ByAppointmentId(int appointmentId)
        {
            await unitOfWork.Patients_VisitingsRepository.DeleteByIdAsync(appointmentId);

        }
    }
}
