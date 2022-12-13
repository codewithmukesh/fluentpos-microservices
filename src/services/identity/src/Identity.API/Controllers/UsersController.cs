using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public void RegisterUser()
    {
    }
}