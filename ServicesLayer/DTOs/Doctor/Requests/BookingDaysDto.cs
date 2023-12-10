using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.DTOs.Doctor.Requests
{
    public class BookingDaysDto
    {
        public string Day { get; set; }
        public List<string> TimeSlots { get; set; }
    }
}
