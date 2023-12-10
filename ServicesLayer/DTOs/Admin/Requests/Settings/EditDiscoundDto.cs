using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.DTOs.Admin.Requests.Settings
{
    public class EditDiscoundDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Requests { get; set; }
        public DiscoundType Type { get; set; }
        public int Value { get; set; }
        public bool Active { get; set; }
    }
}
