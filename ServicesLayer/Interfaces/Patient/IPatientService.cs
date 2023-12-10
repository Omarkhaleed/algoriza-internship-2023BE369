using DomainLayer.DTOs.Paging;
using ServicesLayer.DTOs.Admin.Responses.DoctorsManagementDtos;
using ServicesLayer.DTOs.Doctor.Responses;
using ServicesLayer.DTOs.Paging;
using ServicesLayer.DTOs.Patient.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Interfaces.Patient
{
    public interface IPatientService
    {
        Task<PagingResponse<DoctorsDetailsDto>> GetAllDoctors(PagingRequest paging);
        Task<IQueryable<BookingDetailsDto>> GetAllMyBookingRequests( string PatientId);
        Task<bool> Booking(BookingRequestDto bookingRequest);
        Task<bool> CancelBookingRequest( int bookingId);
    }
}
