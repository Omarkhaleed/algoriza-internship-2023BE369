using AutoMapper;
using DomainLayer.DTOs.Paging;
using DomainLayer.Enums;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.RepositoryPattern;
using ServicesLayer.DTOs.Doctor.Requests;
using ServicesLayer.DTOs.Doctor.Responses;
using ServicesLayer.Interfaces.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Services.Doctor
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<BookingRequest> _repositoryBooking;
        private readonly IRepository<BookingAppointment> _repositoryAppointement;
        private readonly IMapper _mapper;

        public DoctorService(IRepository<BookingRequest> repositoryBooking , IRepository<BookingAppointment> repositoryAppointement , IMapper mapper)
        {
            _repositoryBooking = repositoryBooking;
            _repositoryAppointement = repositoryAppointement;
            _mapper = mapper;
            
        }


        public Task<IQueryable<BookingDetailsDto>> GetAllBookingRequests(PagingRequest paging)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ConfirmCheckUp(int bookingId)
        {
            var bookingRequest = await _repositoryBooking.FindByIdAsync(bookingId);
            bookingRequest.Status = BookingStatus.Completed;
            _repositoryBooking.SaveChanges();
            return true;
        }

        public async Task<bool> AddAppointment(BookingAppointmentDto Appointment)
        {
            var doctorAvailability = new BookingAppointment
            {
                DoctorId = Appointment.DoctorId,
                Price = Appointment.Price,
                BookingDays = Appointment.Days.Select(day =>
                    new BookingDays
                    {
                        Day = Enum.Parse<Days>(day.Day, ignoreCase: true),
                        TimeSlots = day.TimeSlots.Select(timeSlot => new TimeSlot { Time = timeSlot }).ToList()
                    }).ToList()
            };

            
            await _repositoryAppointement.InsertAync(doctorAvailability);
            _repositoryAppointement.SaveChanges();
            return true;
        }
        public async Task<bool> DeleteAppointment(int AppointmentId)
        {
            var BookingTime = await  _repositoryAppointement.FindByIdAsync(AppointmentId);
            // if ( BookingTime != null) { } --> Doesn't have requests return false
            return true;
        }

        public async  Task<bool> EditAppointment(EditAppointmentDto Appointment)
        {
            var oldAppointment = await _repositoryAppointement.FindByIdAsync(Appointment.Id);
            _mapper.Map(Appointment, oldAppointment);

            await _repositoryAppointement.UpdateAsync(oldAppointment);
            _repositoryAppointement.SaveChanges();
            return true;
        }

       
    }
}
