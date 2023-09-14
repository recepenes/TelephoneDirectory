using Microsoft.AspNetCore.Mvc;
using TelephoneDirectory.DataAccessLayer.Records;
using TelephoneDirectory.DataAccessLayer.Services;

namespace TelephoneDirectory.Controllers
{
    [ApiController]
    [Route("api/contacts/")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Route("getAllContacts")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactService.GetAll();
            return Ok(result);
        }

        [Route("getContactDetails/{id:guid:min(1)}")]
        [HttpGet]
        public async Task<IActionResult> GetContactDetail([FromRoute] Guid id)
        {
            var result = await _contactService.GetContactDetail(id);
            return Ok(result);
        }

        [Route("createContact")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContact model)
        {
            await _contactService.Create(model);
            return Ok();
        }

        [Route("deleteContact/{id:guid:min(1)}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _contactService.Delete(id);
            return Ok();
        }

        [Route("createContactInformation/{id:guid:min(1)}")]
        [HttpPost]
        public async Task<IActionResult> CreateContactInformation([FromRoute] Guid id, [FromBody] List<CreateContactInformation> model)
        {
            await _contactService.CreateContactInformation(id, model);
            return Ok();
        }
    }
}
