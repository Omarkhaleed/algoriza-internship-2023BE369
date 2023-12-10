using ServicesLayer.DTOs.Admin.Requests.Settings;
using ServicesLayer.DTOs.Admin.Responses.DoctorsManagementDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Interfaces.Admin
{
    public interface ISettingsService
    {
        Task<bool> AddDiscoundCode(DiscoundDetailsDto Discound);
        Task<bool> EditDiscoundCode(EditDiscoundDto Discound);
        Task<bool> DeleteDiscoundCode(int Id);
        Task<bool> DeactivateDiscoundCode(int Id);
    }
}
