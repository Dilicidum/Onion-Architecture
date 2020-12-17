using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Application.Models.DTO;
using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using System.Linq;
namespace Application.Services
{
    public class DoctorsService : IDoctorsService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        public DoctorsService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddAppointment(Patient_VisitingDTO patient_VisitingDTO)
        {
            Patient_Visiting patient_Visiting = mapper.Map<Patient_Visiting>(patient_VisitingDTO);
            await unitOfWork.Patients_VisitingsRepository.InsertAsync(patient_Visiting);
            await unitOfWork.Commit();
        }

        public async Task AddDoctor(DoctorDTO doctor)
        {
            var doc = mapper.Map<Doctor>(doctor);
            await unitOfWork.DoctorsRepository.InsertAsync(doc);
            await unitOfWork.Commit();
        }

        public async Task AddDoctorSchedule(int doctorId,Doctor_ScheduleDTO doctor_ScheduleDTO)
        {
            Doctor_Schedule doctor_Schedule = mapper.Map<Doctor_Schedule>(doctor_ScheduleDTO);
            doctor_Schedule.DoctorId = doctorId;
            await unitOfWork.Doctors_SheduleRepository.InsertAsync(doctor_Schedule);
            await unitOfWork.Commit();
        }

        public async Task ChangeDoctor(int doctorId, DoctorDTO doctor)
        {
            var doc = await unitOfWork.DoctorsRepository.GetByIdAsync(doctorId);
            var docToChange = mapper.Map<Doctor>(doctor);
            docToChange.Id = doctorId;
            doctor.Id = doctorId;
            mapper.Map(doctor, doc);

            //unitOfWork.DoctorsRepository.Update(doc);
            await unitOfWork.Commit();
        }

        public async Task DeleteAll()
        {
            var doctors = await unitOfWork.DoctorsRepository.GetAsync();
            foreach(var doctor in doctors)
            {
                unitOfWork.DoctorsRepository.Delete(doctor);
            }

            await unitOfWork.Commit();
        }

        public async Task DeleteById(int id)
        {
            await unitOfWork.DoctorsRepository.DeleteByIdAsync(id);
            await unitOfWork.Commit();
        }

        public async Task<IEnumerable<DoctorDTO>> GetAllDoctors()
        {
            var doctors = await unitOfWork.DoctorsRepository.GetAsync();
            var doctorsDTO = mapper.Map<IEnumerable<DoctorDTO>>(doctors);
            return doctorsDTO;
        }

        public async Task<DoctorDTO> GetDoctorById(int id)
        {
            var doctor = await unitOfWork.DoctorsRepository.GetByIdAsync(id);
            var doctorDTO = mapper.Map<DoctorDTO>(doctor);
            return doctorDTO;
        }

        public async Task<IEnumerable<Doctor_ScheduleDTO>> GetDoctorSchedule(int doctorId)
        {
            var schedules = await unitOfWork.Doctors_SheduleRepository.GetAsync(schedule => schedule.DoctorId == doctorId);
            var schedulesDTO = mapper.Map<IEnumerable<Doctor_ScheduleDTO>>(schedules);
            return schedulesDTO;
        }

        public async Task<IEnumerable<DoctorDTO>> GetFreeDoctorsAtDate(DateTime date,string specialization)
        {
            var doctorsInitial = await unitOfWork.DoctorsRepository.GetAsync(null, null, "Doctor_Schedules,Appointments");
            var doctors = doctorsInitial.Where(c => c.SpecializationMapped == specialization);
            var dayOfWeek = date.DayOfWeek;
            List<FreeTime> freeTimes = new List<FreeTime>();
            List<DoctorDTO> doctorDTOs = new List<DoctorDTO>();
            
            foreach (var doctor in doctors)
            {
                List<Patient_Visiting> appointmentsOnExactDay = new List<Patient_Visiting>();
                if (doctor.Appointments != null)
                {
                    appointmentsOnExactDay = doctor.Appointments.Where(c => c != null && c.TimeOfVisit == date).ToList();
                }

                Doctor_Schedule schedule = new Doctor_Schedule();

                TimeSpan startTimeFirstPart = new TimeSpan();
                TimeSpan breakTimeFirstPart = new TimeSpan();
                TimeSpan timeToTakePatient = new TimeSpan();
                TimeSpan temp = new TimeSpan();

                if (doctor.Doctor_Schedules != null)
                {
                    schedule = doctor.Doctor_Schedules.Where(c => c.DayOfWeek == dayOfWeek).FirstOrDefault();
                    if (schedule != null)
                    {
                        startTimeFirstPart = schedule.StartTime;
                        breakTimeFirstPart = schedule.BreakTimeStart;
                        timeToTakePatient = doctor.TimeToTakePatient;
                        temp = startTimeFirstPart;
                        if (schedule.Doctor != null)
                        {
                            while (temp <= breakTimeFirstPart)
                            {
                                var appointmentOnExactTime = appointmentsOnExactDay.FirstOrDefault();
                                if (appointmentOnExactTime == null)
                                {
                                    FreeTime freeTime = new FreeTime()
                                    {
                                        StartTime = temp,
                                        EndTime = temp + timeToTakePatient
                                    };
                                    freeTimes.Add(freeTime);
                                }
                                temp += timeToTakePatient;
                            }
                            TimeSpan startTimeSecondPart = schedule.BreakTimeStart;
                            TimeSpan breakTimeSecondPart = schedule.EndTime;
                            temp = schedule.BreakEndTime;

                            while (temp <= breakTimeSecondPart)
                            {
                                var appointmentOnExactTime = appointmentsOnExactDay.FirstOrDefault();
                                if (appointmentOnExactTime == null)
                                {
                                    FreeTime freeTime = new FreeTime()
                                    {
                                        StartTime = temp,
                                        EndTime = temp + timeToTakePatient
                                    };
                                    freeTimes.Add(freeTime);
                                }
                                temp += timeToTakePatient;
                            }

                            if (freeTimes.Count != 0)
                            {
                                var doctorDTO = mapper.Map<DoctorDTO>(doctor);
                                doctorDTO.FreeTimes = new List<FreeTime>();
                                doctorDTO.FreeTimes.AddRange(freeTimes);
                                doctorDTOs.Add(doctorDTO);
                                freeTimes.Clear();
                            }
                        }
                    }
                }
            }
            return doctorDTOs;
        }
    }
}
