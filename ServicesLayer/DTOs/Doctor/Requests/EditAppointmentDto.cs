using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.DTOs.Doctor.Requests
{
    public class EditAppointmentDto
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public string Time { get; set; }
       
    }
}
