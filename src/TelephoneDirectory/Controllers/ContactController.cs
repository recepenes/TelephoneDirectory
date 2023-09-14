using Microsoft.AspNetCore.Mvc;
using TelephoneDirectory.DataAccessLayer.Records;
using TelephoneDirectory.DataAccessLayer.Services;

namespace TelephoneDirectory.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContact model)
        {
            await _contactService.Create(model);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _contactService.Delete(id);
            return Ok();
        }
    }
}
