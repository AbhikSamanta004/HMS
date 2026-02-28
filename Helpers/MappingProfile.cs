using AutoMapper;
using HMS.API.DTOs;
using HMS.API.Models.Entities;

namespace HMS.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, AuthResponseDto>();

            CreateMap<PatientRequestDto, Patient>();
            CreateMap<Patient, PatientResponseDto>();

            CreateMap<DoctorRequestDto, Doctor>();
            CreateMap<Doctor, DoctorResponseDto>();

            CreateMap<AppointmentRequestDto, Appointment>();
            CreateMap<Appointment, AppointmentResponseDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.Name : ""))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.Name : ""));
        }
    }
}
