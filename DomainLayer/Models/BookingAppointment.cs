using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class BookingAppointment
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public virtual ApplicationUser Doctor { get; set; }
        public int Price { get; set; }
        public virtual ICollection<BookingRequest> BookingRequests { get; set; }
        public virtual ICollection<BookingDays> BookingDays { get; set; }
    }
}
