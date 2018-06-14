using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoSaoBento.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Inválido")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha Inválida")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembrar Senha")]
        public bool RememberMe { get; set; }
    }
}
