using DomainLayer.Enums;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.DTOs.Patient.Requests
{
    public class BookingRequestDto
    {
        public string PatientID { get; set; }
        public int AppointmentId { get; set; }
        public int? BookingDiscoundID { get; set; }
 
    }
}
