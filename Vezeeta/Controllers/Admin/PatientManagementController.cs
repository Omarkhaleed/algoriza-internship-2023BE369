using DomainLayer.DTOs.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.Interfaces.Admin;
using Vezeeta.Filters;

namespace Vezeeta.Controllers.Admin
{
    [Route("api/Admin/PatientManagement")]
    [ApiController]
    [Authorize("Admin")]
    public class PatientManagementController : ControllerBase
    {

        private readonly IPatientManagementService _PatientManagementService;

        public PatientManagementController(IPatientManagementService PatientManagementService)
        {
            _PatientManagementService = PatientManagementService;
        }

        //Task<PagingResponse<PatientDetailsDto>> GetAll(PagingRequest paging);
        //Task<PatientDetailsDto> GetById(string Id);

        [HttpGet("GetAllPatients")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetAllPatients(PagingRequest paging)
        {
            var Result = await _PatientManagementService.GetAll(paging);
            if (Result is null)
            {
                return NotFound("No Patients Found Yet");
            }
            return Ok(Result);
        }

        [HttpGet("GetPatient")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetPatientById(string PatientId)
        {
            var Result = await _PatientManagementService.GetById(PatientId);
            if (Result is null)
            {
                return NotFound("No Doctor found has this Id");
            }
            return Ok(Result);
        }


    }
}
