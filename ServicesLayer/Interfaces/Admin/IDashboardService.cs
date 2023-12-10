using ServicesLayer.DTOs.Admin.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Interfaces.Admin
{
    public interface IDashboardService
    {
        Task<int> NumOfDoctors();
        Task<int> NumOfPatients();
        Task<RequestsNumbersDto> NumOfRequests();
        Task<IQueryable<TopSpecializationsDto>> TopSpecializations( int topNumber);
        Task<IQueryable<TopDoctorsDto>> TopDoctors(int topNumber);
    }
}
