using BookStore.Application.Services;
using BookStore.Contracts;
using BookStore.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequest request)
        {
            await _service.Register(request.UserName,request.Password,request.Email);

            return Ok(request);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody]LoginUserRequest request)
        {
            var token = await _service.Login(request.email, request.password);

            HttpContext.Response.Cookies.Append("tasty-cookies",token);

            return Ok();
        }
    }
}
