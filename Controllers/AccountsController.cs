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
    public class AccountsController : ControllerBase
    {
        private readonly Context _context;

        public AccountsController(Context context)
        {
            _context = context;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            return Ok(await _context.accounts.ToListAsync());
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetById(int id)
        {
            var account = await _context.accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // GET api/<AccountController>/Balance/5
        [HttpGet("GetBalanceById/{id}")]
        public async Task<ActionResult<double>> GetBalanceById(int id)
        {
            var account = await _context.accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account.Balance);
        }

        // GET api/<AccountController>/ExtractAccount/5
        [HttpGet("ExtractAccount/{id}")]
        public async Task<ActionResult<IEnumerable<AccountExtract>>> GetExtract(int id)
        {
            var accountExtract = await _context.extract_account.Where(w=> w.Id_Account == id).ToListAsync();

            if (accountExtract.Count == 0)
            {
                return NotFound();
            }

            return Ok(accountExtract);
        }

        // POST api/<AccountController>/CreateAccount/
        [HttpPost("CreateAccount/")]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] CreateAccountViewModel accountCreate)
        {
            Random randNum = new Random();
            Account account = new Account(accountCreate.Name, randNum.Next(1000, 9999), 0);

            _context.accounts.Add(account);
            await _context.SaveChangesAsync();

            CreatedAtAction("GetById", new { id = account.Id }, account);

            Login login = new Login(account.Id, accountCreate.Email, accountCreate.Password);

            _context.login_account.Add(login);
            await _context.SaveChangesAsync();

            return account;
        }

        // POST api/<AccountController>/TransferBetwenAccounts/
        [HttpPost("TransferBetwenAccounts/")]
        public async Task<ActionResult<Account>> TransferBetwenAccounts([FromBody] TransferViewModel transfer)
        {
            if (AccountExists(transfer.IdDestinyAccount) && AccountExists(transfer.IdOriginAccount))
            {

                AccountExtract extractOrigin = new AccountExtract(transfer.IdOriginAccount, TypeTransferEnum.SAIDA.GetDisplayName(), transfer.Value);
                AccountExtract extractDestiny = new AccountExtract(transfer.IdDestinyAccount, TypeTransferEnum.ENTRADA.GetDisplayName(), transfer.Value);

                var accDestiny = await _context.accounts.FirstAsync(x => x.Id == transfer.IdDestinyAccount);
                var accOrigin = await _context.accounts.FirstAsync(x => x.Id == transfer.IdOriginAccount);

                accDestiny.Balance = Math.Round(accDestiny.Balance + transfer.Value, 2);
                accOrigin.Balance = Math.Round(accOrigin.Balance - transfer.Value, 2);

                if (accDestiny.Balance > 0) { 

                CreatedAtAction("Put", new { id = accDestiny.Id }, accDestiny);
                CreatedAtAction("Put", new { id = accOrigin.Id }, accOrigin);

                await _context.extract_account.AddRangeAsync(extractOrigin, extractDestiny);

                await _context.SaveChangesAsync();

                return accOrigin;
                }
                else
                {
                    return StatusCode(400, "Saldo insulficiente! Não foi possível realizar a transação.");
                }
            }
            else
            {
                return StatusCode(404, "Alguma das contas não foram encontrada!");
            }
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Account>> Put(int id, [FromBody] Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(account);
        }

        private bool AccountExists(int id)
        {
            return _context.accounts.Any(e => e.Id == id);
        }
    }
}
