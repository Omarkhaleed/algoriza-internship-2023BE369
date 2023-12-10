using DomainLayer.DTOs.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.DTOs.Patient.Requests;
using ServicesLayer.Interfaces.Patient;
using Vezeeta.Filters;

namespace Vezeeta.Controllers.Patient
{
    [Route("api/Patient")]
    [ApiController]
    [Authorize("Patient")]
    public class PatientController : ControllerBase
    {

        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        //Task<PagingResponse<DoctorsDetailsDto>> GetAllDoctors(PagingRequest paging);
        //Task<IQueryable<BookingDetailsDto>> GetAllMyBookingRequests(string PatientId);
        //Task<bool> Booking(BookingRequestDto bookingRequest);
        //Task<bool> CancelBookingRequest(int bookingId);

        [HttpGet("GetAllDoctors")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetAllDoctors(PagingRequest paging)
        {
            var Result = await _patientService.GetAllDoctors(paging);
            if (Result is null)
            {
                return NotFound("No Doctors Found Yet");
            }
            return Ok(Result);
        }

        [HttpGet("PatientBookingRequests")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetAllMyBookingRequests(string PatientId)
        {
            var Result = await _patientService.GetAllMyBookingRequests(PatientId);
            if (Result is null)
            {
                return NotFound("No Requests Found Yet");
            }
            return Ok(Result);
        }

        [HttpPost("Booking")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Booking(BookingRequestDto bookingRequest)
        {
            var isBooked = await _patientService.Booking(bookingRequest);
            return isBooked ? Ok(true) : NotFound(false);
        }


        [HttpPost("CancelBooking")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CancelBookingRequest(int bookingId)
        {
            var isCancelled = await _patientService.CancelBookingRequest(bookingId);
            return isCancelled ? Ok(true) : NotFound(false);
        }
    }
}
