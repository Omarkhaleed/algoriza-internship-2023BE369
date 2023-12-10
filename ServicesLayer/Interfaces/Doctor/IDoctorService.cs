using DomainLayer.DTOs.Paging;
using ServicesLayer.DTOs.Doctor.Requests;
using ServicesLayer.DTOs.Doctor.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Interfaces.Doctor
{
    public interface IDoctorService
    {
        Task<IQueryable<BookingDetailsDto>> GetAllBookingRequests(PagingRequest paging);
        Task<bool> ConfirmCheckUp(int bookingId);
        Task<bool> AddAppointment(BookingAppointmentDto Appointment);
        Task<bool>EditAppointment(EditAppointmentDto Appointment);
        Task<bool> DeleteAppointment(int AppointmentId);
    }
}
