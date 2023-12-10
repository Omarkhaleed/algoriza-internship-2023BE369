using DomainLayer.DTOs.Paging;
using ServicesLayer.DTOs.Admin.Responses.DoctorsManagementDtos;
using ServicesLayer.DTOs.Admin.Responses.PatientManagementDtos;
using ServicesLayer.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Interfaces.Admin
{
    public interface IPatientManagementService
    {
        Task<PagingResponse<PatientDetailsDto>> GetAll(PagingRequest paging);
        Task<PatientDetailsDto> GetById(string Id);
    }
}
