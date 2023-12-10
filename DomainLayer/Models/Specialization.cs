using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Specialization
    {
       public int SpecializationId { get; set; }
       public string Name { get; set; }
        public virtual ICollection<ApplicationUser> Doctors { get; set; }
    }
}
