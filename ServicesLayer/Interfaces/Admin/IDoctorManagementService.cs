using DomainLayer.DTOs.Paging;
using ServicesLayer.DTOs.Admin.Requests.DoctorsManagementDtos;
using ServicesLayer.DTOs.Admin.Responses;
using ServicesLayer.DTOs.Admin.Responses.DoctorsManagementDtos;
using ServicesLayer.DTOs.Authentication.Responses;
using ServicesLayer.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Interfaces.Admin
{
   public interface IDoctorManagementService
    {
        Task<PagingResponse<DoctorsDetailsDto>> GetAllDoctors(PagingRequest paging);
        Task<DoctorsDetailsDto> GetDoctorById(string Id);
        Task<AuthenticationResponseDto> AddDoctor(DoctorInfoDto Doctor);
        Task<bool> AddSpecilaization(AddSpecializationDto Name);
        Task<AuthenticationResponseDto> EditDoctor(EditDoctorDto Doctor);
        Task<bool> DeleteDoctor(string Id);
    }
}
