using AutoMapper;
using DomainLayer.Models;
using ServicesLayer.DTOs.Authentication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Mappings
{
    public class AuthenticationMappingProfile : Profile
    {
        public AuthenticationMappingProfile()
        {
            CreateMap<RegisterRequestDto, ApplicationUser>();
        }
    }

}