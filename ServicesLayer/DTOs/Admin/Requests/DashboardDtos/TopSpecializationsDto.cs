using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.DTOs.Admin.Responses.DashboardDtos
{
    public class TopSpecializationsDto
    {
        public string SpecializationName { get; set; }
        public int TotaRequests { get; set; }

    }
}
