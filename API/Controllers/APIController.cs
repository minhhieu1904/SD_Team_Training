using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class APIController : ControllerBase
    {
        protected string userName => User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}