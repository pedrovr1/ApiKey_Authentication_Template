using Microsoft.AspNetCore.Mvc;
using WebApi_ApiKeyAuthentication.Attributes;

namespace WebApi_ApiKeyAuthentication.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet("/api/login")]
        public ActionResult GetUser()
        {
            return Ok("This is the secret!");
        }

    }
}