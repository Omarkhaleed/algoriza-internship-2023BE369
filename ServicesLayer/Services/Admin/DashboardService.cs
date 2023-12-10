using AutoMapper;
using DomainLayer.Enums;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RepositoryLayer;
using RepositoryLayer.RepositoryPattern;
using ServicesLayer.DTOs.Admin.Responses;
using ServicesLayer.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Services.Admin
{
    public class DashboardService : IDashboardService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Specialization> _repository;
        private readonly IRepository<BookingRequest> _BookingRequestRepository;
        private readonly IRepository<BookingAppointment> _BookingAppointmentsRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _DbContext;

        public DashboardService(UserManager<ApplicationUser> userManager, IRepository<Specialization> repository, IRepository<BookingRequest> BookingRequestRepository, IMapper mapper, ApplicationDbContext DbContext)
        {
            _userManager = userManager;
            _repository = repository;
            _BookingRequestRepository = BookingRequestRepository;
            _DbContext = DbContext;
            _mapper = mapper;

        }
        public async Task<int> NumOfDoctors()
        {
            var Doctors = await _userManager.GetUsersInRoleAsync("Doctor");
            int totalCount = Doctors.Count();

            return totalCount;
        }

        public async Task<int> NumOfPatients()
        {
            var Patients = await _userManager.GetUsersInRoleAsync("Patient");
            int totalCount = Patients.Count();

            return totalCount;
        }

        public async Task<RequestsNumbersDto> NumOfRequests()
        {
            int Requests = await _BookingRequestRepository.CountAsync();

            Expression<Func<BookingRequest, bool>> pendingExpression =
                p => p.Status == BookingStatus.Pending;

            int PendingRequests = await _BookingRequestRepository.CountAsync(pendingExpression);

            Expression<Func<BookingRequest, bool>> CompletedExpression =
                p => p.Status == BookingStatus.Completed;

            int CompletedRequests = await _BookingRequestRepository.CountAsync(CompletedExpression);

            Expression<Func<BookingRequest, bool>> CancelledExpression =
                p => p.Status == BookingStatus.Cancelled;

            int CancelledRequests = await _BookingRequestRepository.CountAsync(CancelledExpression);
            var Result = new RequestsNumbersDto
            {
                TotalRequests = Requests,
                CompletedRequests = PendingRequests,
                CancelledRequests = PendingRequests


            };

            return Result;
        }

        public async Task<IQueryable<TopDoctorsDto>> TopDoctors(int topNumber)
        {
            var topDoctors = await _DbContext.bookingAppointments
            .OrderByDescending(b => b.BookingRequests.Count)
            .Take(topNumber)
            .Select(b => new TopDoctorsDto
            {
                Image = b.Doctor.Image,
                FullName = $"{b.Doctor.FirstName} {b.Doctor.LastName}",
                Requests = b.BookingRequests.Count,
                Specialize = b.Doctor.Specialization.Name

            })
            .ToListAsync();

            return topDoctors.AsQueryable();
        }

        public async Task<IQueryable<TopSpecializationsDto>> TopSpecializations(int topNumber)
        {
            var topSpecializations = await _DbContext.specializations
        .OrderByDescending(s => s.Doctors.Sum(d => d.Appointments.Sum(a => a.BookingRequests.Count)))
        .Take(topNumber)
        .Select(s => new TopSpecializationsDto
        {

            SpecializationName = s.Name,
            // Add other properties you want to include in the DTO
        })
        .ToListAsync();

            return topSpecializations.AsQueryable();
        }
    }
}
