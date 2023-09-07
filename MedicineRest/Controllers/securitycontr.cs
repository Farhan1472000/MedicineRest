/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebRestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
    }
}*/

//https://github.com/Farhan1472000/MedicineRest
using MedicineRest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MedicineRest.Models.securityservic;

namespace WebRestApp.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        IConfiguration config;
        securityservic service;
        public SecurityController(IConfiguration _config, securityservic service)
        {
            config = _config;
            this.service = service;
        }



        [HttpPost]
        [Route("login")]
        public IActionResult Login(OperatorLoginModel model)
        {
            TokenAndRole? tokenAndRole = service.AuthenticateUserAndGetToken(model);
            if (tokenAndRole == null)
            {
                return BadRequest("Invalid UserName or Password..");
            }
            else
                return Ok(tokenAndRole);
        }
    }
}
