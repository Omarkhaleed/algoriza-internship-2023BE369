using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class BookingDays
    {
        public int Id { get; set; }
        public Days Day { get; set; }
        public int BookingAppointmentId { get; set; }
        public virtual BookingAppointment BookingAppointment { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }


    }
}
