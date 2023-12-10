using AutoMapper;
using DomainLayer.DTOs.Paging;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.RepositoryPattern;
using ServicesLayer.DTOs.Admin.Responses.DoctorsManagementDtos;
using ServicesLayer.DTOs.Admin.Responses.PatientManagementDtos;
using ServicesLayer.DTOs.Paging;
using ServicesLayer.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Services.Admin
{
    public class PatientManagementService : IPatientManagementService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<BookingRequest> _repositoryBooking;

        private readonly IRepository<Specialization> _repository;
        private readonly IMapper _mapper;

        public PatientManagementService(UserManager<ApplicationUser> userManager,IRepository<Specialization> repository, IMapper mapper)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<PagingResponse<PatientDetailsDto>> GetAll(PagingRequest paging)
        {
            var Patients = await _userManager.GetUsersInRoleAsync("Patient");

            // Paging logic
            var totalCount = Patients.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / paging.pageSize);

            var someOfDoctors = Patients
                .Skip((paging.page - 1) * paging.pageSize)
            .Take(paging.pageSize);

            var Result = someOfDoctors.Select(p => _mapper.Map<PatientDetailsDto>(p));


            var pagedResult = new PagingResponse<PatientDetailsDto>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = paging.page,
                PageSize = paging.pageSize,
                Data = Result.ToList()
            };

            return pagedResult;
        }

        public async Task<PatientDetailsDto> GetById(string Id)
        {
            //var Patient = await _userManager.FindByIdAsync(Id);

            var Patient = await _repositoryBooking.FindByConditionAsync
                 (P => P.PatientID.Equals(Id),
                 includes: query => query.Include(P => P.Patient).ThenInclude(p => p.Appointments));

            var result = _mapper.Map<PatientDetailsDto>(Patient);
            return result;
        }
    }
}
