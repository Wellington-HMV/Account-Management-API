using Microsoft.EntityFrameworkCore;

namespace API_PDV.Context
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options) : base(options) { }


        public DbSet<TESTE.API.Models.Account> accounts { get; set; }
        public DbSet<TESTE.API.Models.AccountExtract> extract_account { get; set; }
        public DbSet<TESTE.API.Models.Login> login_account { get; set; }
    }
}
