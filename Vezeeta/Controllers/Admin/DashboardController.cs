
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.Interfaces.Admin;
using Vezeeta.Filters;
namespace Vezeeta.Controllers.Admin
{
    [Route("api/Admin/Dashboard")]
    [ApiController]
    [Authorize("Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _DashboardService;
       
        public DashboardController(IDashboardService DashboardService)
        {
            _DashboardService = DashboardService;

        }


        //Task<int> NumOfDoctors();
        //Task<int> NumOfPatients();
        //Task<RequestsNumbersDto> NumOfRequests();
        //Task<IQueryable<TopSpecializationsDto>> TopSpecializations(int topNumber);
        //Task<IQueryable<TopDoctorsDto>> TopDoctors(int topNumber);


        [HttpGet("NumOfDoctors")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetNumOfDoctors()
        {
            var Result = await _DashboardService.NumOfDoctors();
            return Ok(Result);
        }

        [HttpGet("NumOfPatients")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetNumOfPatients()
        {
            var Result = await _DashboardService.NumOfPatients();
            return Ok(Result);
        }


        [HttpGet("NumOfRequests")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetNumOfRequests()
        {
            var Result = await _DashboardService.NumOfRequests();
            return Ok(Result);
        }

        [HttpGet("TopSpecialization")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetTopSpecializations(int Number)
        {
            var Result = await _DashboardService.TopSpecializations(Number);
            return Ok(Result);
        }

        [HttpGet("TopDoctors")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetTopDoctors(int Number)
        {
            var Result = await _DashboardService.TopDoctors(Number);
            return Ok(Result);
        }

    }
}
