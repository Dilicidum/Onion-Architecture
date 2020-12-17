using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Entities;
using Application.Models.DTO;
namespace Application.MappingProfile
{
    public class ModelToResource:Profile
    {
        public ModelToResource()
        {
            CreateMap<Patient, PatientDTO>();
            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dest => dest.Post, act => act.MapFrom(src => src.PostMapped))
                .ForMember(dest => dest.Specialization, act => act.MapFrom(src => src.SpecializationMapped))
                .ForMember(dest => dest.TimeToTakePatient, act => act.MapFrom(src => src.TimeToTakePatientMapped));
            CreateMap<Doctor_Schedule, Doctor_ScheduleDTO>();
            CreateMap<Patient_Visiting, Patient_VisitingDTO>();
        }
    }
}
