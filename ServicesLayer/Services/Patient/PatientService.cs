using AutoMapper;
using DomainLayer.DTOs.Paging;
using DomainLayer.Enums;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RepositoryLayer.RepositoryPattern;
using ServicesLayer.DTOs.Admin.Responses.DoctorsManagementDtos;
using ServicesLayer.DTOs.Admin.Responses.PatientManagementDtos;
using ServicesLayer.DTOs.Doctor.Responses;
using ServicesLayer.DTOs.Paging;
using ServicesLayer.DTOs.Patient.Requests;
using ServicesLayer.Interfaces.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Services.Patient
{
    public class PatientService : IPatientService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<BookingRequest> _repository;
        private readonly IMapper _mapper;

        public PatientService(UserManager<ApplicationUser> userManager,IRepository<BookingRequest> repository, IMapper mapper)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<PagingResponse<DoctorsDetailsDto>> GetAllDoctors(PagingRequest paging)
        {
            var Doctors = await _userManager.GetUsersInRoleAsync("Doctor");

            // Paging logic
            var totalCount = Doctors.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / paging.pageSize);

            var someOfDoctors = Doctors
                .Skip((paging.page - 1) * paging.pageSize)
            .Take(paging.pageSize);

            var Result = someOfDoctors.Select(p => _mapper.Map<DoctorsDetailsDto>(p));


            var pagedResult = new PagingResponse<DoctorsDetailsDto>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = paging.page,
                PageSize = paging.pageSize,
                Data = Result.ToList()
            };

            return pagedResult;
        }

    public async Task<IQueryable<BookingDetailsDto>> GetAllMyBookingRequests(string PatientId)
    {
            var BookingRequests = await _repository.FindByConditionAsync(p => p.PatientID.Equals(PatientId),
                 includes: query => query.Include(P => P.Appointment).Include(p => p.BookingDiscound));

            var result = BookingRequests.Select(p => _mapper.Map<BookingDetailsDto>(p));
           
           return result;
    }
        public async Task<bool> Booking(BookingRequestDto bookingRequest)
        {
            var addedRequest = _mapper.Map<BookingRequest>(bookingRequest);
                 await _repository.InsertAync(addedRequest);
            _repository.SaveChanges();
            return true;
        }

        public async Task<bool> CancelBookingRequest(int bookingId)
        {
            var bookingRequest = await _repository.FindByIdAsync(bookingId);
            bookingRequest.Status = BookingStatus.Cancelled;
            _repository.SaveChanges();
            return true;
        }

      
    }
}
