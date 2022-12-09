using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Services.Identity.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public void RegisterUser()
    {
    }
}