using Microsoft.AspNetCore.Mvc;
using SwiftProject.Services;

namespace SwiftProject.Controllers
{
    [ApiController]
    [Route("api/mt103")]
    public class MT103Controller(IMT103Service service) : ControllerBase
    {
        private readonly IMT103Service _service = service;

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            await _service.SaveAsync(file);
            return Ok("MT103 Saved successfully!");
        }
    }
}
