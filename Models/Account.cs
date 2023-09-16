using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESTE.API.Models
{
    public class Account
    {
        public Account(string name, int accountNumber, double balance)
        {
            Name = name;
            AccountNumber = accountNumber;
            Balance = balance;
        }

        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("accountNumber")]
        public int AccountNumber { get; set; }


        [JsonProperty("balance")]
        public double Balance { get; set; }
    }

    public class AccountExtract
    {
        public AccountExtract(int id_Account, string type, double value)
        {
            Id_Account = id_Account;
            Type = type;
            Value = value;
            Register = DateTime.Now;
        }

        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("FK_ACCOUNT_EXTRACT")]
        [JsonProperty("id_account")]
        public int Id_Account { get; set; }

        [Required]
        [JsonProperty("type")]
        public string Type { get; set; }

        [Required]
        [JsonProperty("value")]
        public double Value { get; set; }

        [Required]
        [JsonProperty("register")]
        public DateTime Register { get; set; }
    }
}
