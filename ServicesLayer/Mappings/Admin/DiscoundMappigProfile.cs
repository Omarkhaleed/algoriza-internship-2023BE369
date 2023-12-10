using AutoMapper;
using DomainLayer.Models;
using ServicesLayer.DTOs.Admin.Requests.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Mappings.Admin
{
    public class DiscoundMappigProfile:Profile
    {
        public DiscoundMappigProfile()
        {

            CreateMap<DiscoundDetailsDto, Discound>();
            CreateMap<EditDiscoundDto, Discound>();

        }
    }
}
