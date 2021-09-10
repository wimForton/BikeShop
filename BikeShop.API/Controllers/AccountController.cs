using BikeShop.BLL.Services.Account;
using BikeShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BikeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            if (login == null)
            {
                return BadRequest("Invalid client request");
            }
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44387",
                    audience: "http://localhost:44387",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { token = tokenString, user = user });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] LoginViewModel model)
        {
            var user = new IdentityUser { UserName = model.UserName, PasswordHash = model.Password };
            var result = _userManager.CreateAsync(user, model.Password);
            if (!result.Result.Succeeded)
            {
                return BadRequest(result.Result.Errors);
            }

            return Ok();
        }
    }
}
