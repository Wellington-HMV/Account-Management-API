using API_PDV.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using TESTE.API.Enums;
using TESTE.API.Models;
using TESTE.API.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TESTE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        // POST api/<AccountController>Login
        [HttpPost]
        public async Task<ActionResult<Account>> Login([FromBody] LoginViewModel login)
        {
            if (LoginExists(login.Email, login.Password))
            {
                var logged = await _context.login_account.FirstAsync(f => f.Password == login.Password && f.Email == login.Email);
                return Ok(await _context.accounts.FindAsync(logged.Id_Account));
            }
            else
            {
                return StatusCode(404, "Email ou senha está incorreto!");
            }
        }
        private bool LoginExists(string email, string password)
        {
            return _context.login_account.Any(e => e.Email == email && e.Password == password);
        }
    }
}
