using Microsoft.AspNetCore.Mvc;

namespace TelephoneDirectory.Controllers
{
    [ApiController]
    [Route("api/enums")]
    public class EnumController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var contactInformationTypeEnum = Enum.GetValues(typeof(ContactInformationTypeEnum))
                .Cast<ContactInformationTypeEnum>()
                .ToDictionary(t => (int)t, t => t.ToString());

            return Ok(
                new { contactInformationTypeEnum }
            );
        }
    }
}
