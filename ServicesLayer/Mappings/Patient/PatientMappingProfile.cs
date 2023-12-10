using AutoMapper;
using DomainLayer.Models;
using ServicesLayer.DTOs.Admin.Responses.DoctorsManagementDtos;
using ServicesLayer.DTOs.Admin.Responses.PatientManagementDtos;
using ServicesLayer.DTOs.Patient.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Mappings.Patient
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile() {


            CreateMap<ApplicationUser, PatientDetailsDto>();
            CreateMap<BookingRequestDto, BookingRequest>();
        }
    }
}
