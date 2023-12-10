using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class BookingRequest
    {
        public int Id { get; set; }
        public string PatientID { get; set; }
        public virtual ApplicationUser Patient { get; set; }
        public int BookingAppointmentId { get; set; }
        public virtual BookingAppointment Appointment { get; set; }
        public int BookingDiscoundID { get; set; }
        public virtual Discound BookingDiscound { get; set; }
        public BookingStatus Status { get; set; }
        public double  FinalPrice { get; set; }
      
      
        
    }
}
