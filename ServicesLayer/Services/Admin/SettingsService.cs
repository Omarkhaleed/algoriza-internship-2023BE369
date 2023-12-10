using AutoMapper;
using DomainLayer.Models;
using Microsoft.Extensions.Hosting;
using RepositoryLayer.RepositoryPattern;
using ServicesLayer.DTOs.Admin.Requests.Settings;
using ServicesLayer.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServicesLayer.Services.Admin
{
    public class SettingsService : ISettingsService
    {

        private readonly IRepository<Discound> _repository;
        private readonly IMapper _mapper;

        public SettingsService(IRepository<Discound> repository , IMapper mapper)

        {
            _repository = repository;
            _mapper = mapper;
                
        }

        public async  Task<bool> AddDiscoundCode(DiscoundDetailsDto Discound)
        {
            var  AddedDiscound = _mapper.Map<Discound>(Discound);
            await _repository.InsertAync(AddedDiscound);
            _repository.SaveChanges();
            return true;
        }

        public async Task<bool> DeactivateDiscoundCode(int Id)
        {
            var Discound = await _repository.FindByIdAsync(Id);
            Discound.Active = false;
            _repository.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteDiscoundCode(int Id)
        {
            var Discound = await _repository.FindByIdAsync(Id);
            if (Discound is null) return false;

            await _repository.DeleteAsync(Discound);
            _repository.SaveChanges();
            return true;
        }

        public async Task<bool> EditDiscoundCode(EditDiscoundDto Discound)
        {
            var oldDiscound = await _repository.FindByIdAsync(Discound.Id);
            _mapper.Map(Discound, oldDiscound);
           
            await _repository.UpdateAsync(oldDiscound);
            _repository.SaveChanges();
            return true;
        }
    }
}
