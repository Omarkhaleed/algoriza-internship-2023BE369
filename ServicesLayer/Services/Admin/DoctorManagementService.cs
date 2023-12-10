using AutoMapper;
using DomainLayer.DTOs.Paging;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using RepositoryLayer.RepositoryPattern;
using ServicesLayer.DTOs.Admin.Requests.DoctorsManagementDtos;
using ServicesLayer.DTOs.Admin.Responses.DoctorsManagementDtos;
using ServicesLayer.DTOs.Authentication.Responses;
using ServicesLayer.DTOs.Paging;
using ServicesLayer.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Services.Admin
{
    public class DoctorManagementService : IDoctorManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Specialization> _repository;
        private readonly IMapper _mapper;

        public DoctorManagementService(UserManager<ApplicationUser> userManager,IRepository<Specialization> repository, IMapper mapper)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
           
        }
        public async Task<AuthenticationResponseDto> AddDoctor(DoctorInfoDto Doctor)
        {
            if (await _userManager.FindByEmailAsync(Doctor.Email) is not null)
                return new AuthenticationResponseDto { Message = "This Email is already exist" };

           
            // Upload Image as a file and save the path in image column
            //if (Doctor.FormFile != null)
            //{
            //    var filePath = SaveImageFile(Doctor.FormFile);
            //    Doctor.Image = filePath;
            //}

            var user = _mapper.Map<ApplicationUser>(Doctor);
            user.UserName = ExtractUsernameFromEmail(user.Email);
            var result = await _userManager.CreateAsync(user, Doctor.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthenticationResponseDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "Doctor");

            // send Email to doctor with username and password
            await SendEmailToDoctor(user.Email, user.UserName, Doctor.Password);

            return new AuthenticationResponseDto { IsAuthenticated = true };
           

        }
       
        public async Task<bool> AddSpecilaization(AddSpecializationDto Name)
        {
            var specialization = _mapper.Map<Specialization>(Name);
             await _repository.InsertAync(specialization);
            _repository.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteDoctor(string Id)
        {
            var Doctor = await _userManager.FindByIdAsync(Id);
            if (Doctor is null) return false;
            // check if doctor has requests  ---> Not done yet
             await _userManager.DeleteAsync(Doctor);   
            _repository.SaveChanges();
            return true;
        }

        public async Task<AuthenticationResponseDto> EditDoctor(EditDoctorDto Doctor)
        {
            if (await _userManager.FindByIdAsync(Doctor.Id) is not null)
                return new AuthenticationResponseDto { Message = "This Doctor is already exist" };


            var user = _mapper.Map<ApplicationUser>(Doctor);
          
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthenticationResponseDto { Message = errors };
            }

            return new AuthenticationResponseDto { IsAuthenticated = true };
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

        public async Task<DoctorsDetailsDto> GetDoctorById(string Id)
        {
            var Doctor = await _userManager.FindByIdAsync(Id);
            var result = _mapper.Map<DoctorsDetailsDto>(Doctor);
            return result;
        }



        private string ExtractUsernameFromEmail(string email)
        {

            int atIndex = email.IndexOf('@');
            return email.Substring(0, atIndex);
        }


        private async Task SendEmailToDoctor(string toEmail, string username, string password)
        {
           
            using (var client = new SmtpClient("smtp.gmail.com"))
            {
                var message = new MailMessage
                {
                    From = new MailAddress("omarkhaalid76@gmail.com"),
                    Subject = " Vezeeta Registration Confirmation",
                    Body = $"Your username: {username}\nYour password: {password}"
                };

                message.To.Add(toEmail);

                await client.SendMailAsync(message);
            }
        }


        //private string SaveImageFile(IFormFile formFile)
        //{
            
        //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
        //    var filePath = Path.Combine("/Vezeeta/Images", fileName);

        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        formFile.CopyTo(fileStream);
        //    }

        //    return "/Images/" + fileName; 
        //}
    }
}
