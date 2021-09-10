using BikeShop.BLL.Services.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BikeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _AccountService;
        public AccountController(IAccountService AccountService)
        {
            _AccountService = AccountService;
        }
        //GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<IdentityUser> Get()
        {
            return _AccountService.GetAccounts();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<IdentityUser> Get(string id)
        {
            return await _AccountService.GetAccountByIdAsync(id);
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
