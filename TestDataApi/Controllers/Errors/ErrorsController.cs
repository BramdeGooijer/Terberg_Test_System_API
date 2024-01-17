using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestDataApi.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [AllowAnonymous]
    public IActionResult Error()
    {
        return Problem();
    }
}