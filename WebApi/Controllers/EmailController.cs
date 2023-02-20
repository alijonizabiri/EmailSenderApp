using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.EmailService;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _service;

        public EmailController(IEmailService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult SendEmail(UserEmailOptions emailDto)
        {

            _service.SendTestEmail(emailDto);
            return Ok(); 
        }
    }
}
