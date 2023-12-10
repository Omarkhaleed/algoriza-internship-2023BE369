﻿using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.DTOs.Admin.Responses.PatientManagementDtos
{
    public class PatientDetailsDto
    {

        public string? Image { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<RequestsDetailsDto> Requests { get; set; }
    }
}
