using DomainLayer.DTOs.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.DTOs.Admin.Requests.DoctorsManagementDtos;
using ServicesLayer.Interfaces.Admin;
using Vezeeta.Filters;

namespace Vezeeta.Controllers.Admin
{
    [Route("api/Admin/DoctorManagement")]
    [ApiController]
    [Authorize("Admin")]
    public class DoctorManagementController : ControllerBase
    {

        private readonly IDoctorManagementService _DoctorManagementService;

        public DoctorManagementController(IDoctorManagementService DoctorManagementService)
        {
            _DoctorManagementService = DoctorManagementService;
        }


        //Task<PagingResponse<DoctorsDetailsDto>> GetAllDoctors(PagingRequest paging);
        //Task<DoctorsDetailsDto> GetDoctorById(string Id);
        //Task<AuthenticationResponseDto> AddDoctor(DoctorInfoDto Doctor);
        //Task<bool> AddSpecilaization(AddSpecializationDto Name);
        //Task<AuthenticationResponseDto> EditDoctor(EditDoctorDto Doctor);
        //Task<bool> DeleteDoctor(string Id);

        [HttpGet("GetAllDoctors")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetAllDoctors(PagingRequest paging)
        {
            var Result = await _DoctorManagementService.GetAllDoctors(paging);
            if (Result is null)
            {
                return NotFound("No Doctors Found Yet");
            }
            return Ok(Result);
        }

        [HttpGet("GetDoctor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetDoctorById(string DoctorId)
        {
            var Result = await _DoctorManagementService.GetDoctorById(DoctorId);
            if (Result is null)
            {
                return NotFound("No Doctor found has this Id");
            }
            return Ok(Result);
        }

        [HttpPost("AddDoctor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDoctor(DoctorInfoDto Doctor)
        {
            var Result = await _DoctorManagementService.AddDoctor(Doctor);
            return Ok(Result);

        }

        [HttpPost("AddSpecialization")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddSpecialization(AddSpecializationDto Name)
        {
            var Result = await _DoctorManagementService.AddSpecilaization(Name);
            return Ok(Result);


        }
      
        [HttpPut("EditDoctor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditDoctor(EditDoctorDto Doctor)
        {
            var Result = await _DoctorManagementService.EditDoctor(Doctor);
            return Ok(Result);

        }

        [HttpDelete("DeleteDoctor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteDoctor(string Id)
        {
            var isDeleted = await _DoctorManagementService.DeleteDoctor(Id);
            return isDeleted ? NoContent() : NotFound("Sorry The Post Not Found");

        }
    }
}
