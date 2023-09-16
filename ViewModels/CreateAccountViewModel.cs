using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TESTE.API.ViewModels
{
    public class CreateAccountViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
