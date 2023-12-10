using AutoMapper;
using DomainLayer.Models;
using ServicesLayer.DTOs.Admin.Requests.DoctorsManagementDtos;
using ServicesLayer.DTOs.Admin.Responses.DoctorsManagementDtos;
using ServicesLayer.DTOs.Authentication.Requests;
using ServicesLayer.DTOs.Doctor.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Mappings.Doctor
{
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {
            CreateMap<DoctorInfoDto, ApplicationUser>();
            CreateMap<ApplicationUser, DoctorsDetailsDto>();
            CreateMap<AddSpecializationDto, Specialization>();
            CreateMap<BookingAppointmentDto, BookingAppointment>();
            CreateMap<EditAppointmentDto, BookingAppointment>();
        }
    }
}
