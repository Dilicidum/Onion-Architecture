using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Application.Models.DTO;
using Domain.Entities;
namespace Application.MappingProfile
{
    public class ResourceToModel:Profile
    {
        public ResourceToModel()
        {
            CreateMap<PatientDTO, Patient>();
            CreateMap<DoctorDTO, Doctor>()
                .ForMember(dest => dest.TimeToTakePatientMapped, act => act.MapFrom(src => src.TimeToTakePatient))
                .ForMember(dest => dest.PostMapped, act => act.MapFrom(src => src.Post))
                .ForMember(dest => dest.SpecializationMapped, act => act.MapFrom(src => src.Specialization))
                .ForMember(dest=>dest.TimeToTakePatient,act=>act.Ignore())
                .ForMember(dest=>dest.Post,act=>act.Ignore())
                .ForMember(dest=>dest.Specialization,act=>act.Ignore());
            CreateMap<Doctor_ScheduleDTO, Doctor_Schedule>();
            CreateMap<Patient_VisitingDTO, Patient_Visiting>();
        }
    }
}
