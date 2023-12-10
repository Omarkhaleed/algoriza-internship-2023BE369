using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Discound
    {
        public int DiscoundId { get; set; }
        public string Code { get; set; }
        public int Requests { get; set; }
        public  DiscoundType  Type { get; set; }
        public  int  Value { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<BookingRequest> BookingRequests { get; set; }
    }

}
