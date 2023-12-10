using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.DTOs.Admin.Responses.PatientManagementDtos
{
    public class RequestsDetailsDto
    {
        public string? Image { get; set; }
        public string DoctorName { get; set; }
        public string Specialize { get; set; }
        public Days Day { get; set; }
        public string Time { get; set; }
        public int price { get; set; }
        public string? DiscoundCode { get; set; }
        public double FinalPrice { get; set; }
        public BookingStatus Status { get; set; }
    }
}
