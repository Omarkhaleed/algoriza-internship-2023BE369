using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.DTOs.Admin.Requests.Settings;
using ServicesLayer.Interfaces.Admin;
using Vezeeta.Filters;

namespace Vezeeta.Controllers.Admin
{
    [Route("api/Admin/Settings")]
    [ApiController]
    [Authorize("Admin")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _SettingsService;

        public SettingsController(ISettingsService SettingsService)
        {
            _SettingsService = SettingsService;
        }

        //Task<bool> AddDiscoundCode(DiscoundDetailsDto Discound);
        //Task<bool> EditDiscoundCode(EditDiscoundDto Discound);
        //Task<bool> DeleteDiscoundCode(int Id);
        //Task<bool> DeactivateDiscoundCode(int Id);

        [HttpPost("AddDiscoundCode")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDiscoundCode(DiscoundDetailsDto Discound)
        {
            var Result = await _SettingsService.AddDiscoundCode(Discound);
            return Ok(Result);
        }

        [HttpPut("EditDiscoundCode")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditDiscoundCode(EditDiscoundDto Discound)
        {
            var isUpdated = await _SettingsService.EditDiscoundCode(Discound);
            return isUpdated ? NoContent() : NotFound("Sorry There is Error");

        }

        [HttpDelete("DeleteDiscoundCode")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteDiscoundCode(int Id)
        {
            var isDeleted = await _SettingsService.DeleteDiscoundCode(Id);
            return isDeleted ? Ok(true) : NotFound(false);

        }

        [HttpPost("DeactivateDiscoundCode")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeactivateDiscoundCode(int Id)
        {
            var isDeactivated = await _SettingsService.DeactivateDiscoundCode(Id);
            return isDeactivated ? Ok(true) : NotFound(false);

        }
    }
}
