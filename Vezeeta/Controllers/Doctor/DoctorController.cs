using DomainLayer.DTOs.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.DTOs.Doctor.Requests;
using ServicesLayer.Interfaces.Doctor;
using Vezeeta.Filters;

namespace Vezeeta.Controllers.Doctor
{
    [Route("api/Doctor")]
    [ApiController]
    [Authorize("Doctor")]
    public class DoctorController : ControllerBase
    {
        //Task<IQueryable<BookingDetailsDto>> GetAllBookingRequests(PagingRequest paging);
        //Task<bool> ConfirmCheckUp(int bookingId);
        //Task<bool> AddAppointment(BookingAppointmentDto Appointment);
        //Task<bool> EditAppointment(EditAppointmentDto Appointment);
        //Task<bool> DeleteAppointment(int AppointmentId);

        private readonly IDoctorService _DoctorService;
        public DoctorController(IDoctorService DoctorService)
        {
            _DoctorService = DoctorService;

        }

        [HttpGet("GetAllBookingRequests")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetAllBookingRequests(PagingRequest paging)
        {
            var Result = await _DoctorService.GetAllBookingRequests(paging); 
            if (Result == null) { return NotFound(); }
            return Ok(Result);
        }

        [HttpPost("ConfirmBooking")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ConfirmCheckUp(int bookingId)
        {
            var isConfirmed = await _DoctorService.ConfirmCheckUp(bookingId);
            return isConfirmed ? Ok(true) : NotFound(false);
        }

        [HttpPost("AddAppointment")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddAppointment(BookingAppointmentDto Appointment) 
        { 
        var isAdded =  await _DoctorService.AddAppointment(Appointment);
            return isAdded ? Ok(true) : NotFound(false);
        }

        [HttpPut("UpdateAppointment")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditAppointment(EditAppointmentDto Appointment)
        {
            var isEdited = await _DoctorService.EditAppointment(Appointment);
            return isEdited ? Ok(true) : NotFound(false);
        }

        [HttpDelete("DeleteAppointment")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteAppointment(int AppointmentId)
        {
            var isDeleted = await _DoctorService.DeleteAppointment(AppointmentId);
            return isDeleted ? Ok(true) : NotFound(false);
        }



    }

}

