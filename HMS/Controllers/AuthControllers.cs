using HMS.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthControllers(IAuthService authService) : ControllerBase
    {



    }
}
