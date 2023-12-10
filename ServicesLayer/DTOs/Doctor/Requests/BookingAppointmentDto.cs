using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.DTOs.Doctor.Requests
{
    public class BookingAppointmentDto
    {
        public string DoctorId { get; set; }
        public List<BookingDaysDto> Days { get; set; }
        public int Price { get; set; }
    }
}
