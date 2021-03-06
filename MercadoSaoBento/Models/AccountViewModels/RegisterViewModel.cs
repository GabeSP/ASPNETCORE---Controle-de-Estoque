﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoSaoBento.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name ="Nome")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha deve conter pelo menos uma letra Maíusculo, um Número e um Símbolo(@*#&-!)", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As Senhas não correspondem")]
        public string ConfirmPassword { get; set; }
    }
}
