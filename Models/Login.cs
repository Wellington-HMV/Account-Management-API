using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESTE.API.Models
{
    public class Login
    {

        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("FK_ACCOUNT_LOGIN")]
        [JsonProperty("id_account")]
        public int Id_Account { get; set; }

        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        public Login(int id_Account, string email, string password)
        {
            Id_Account = id_Account;
            Email = email;
            Password = password;
        }
    }

}
