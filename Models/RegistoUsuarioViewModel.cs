using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaPessoal_BD.Models
{
    public class RegistoUsuarioViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "A Password não coincide")]
        [Display(Name = "Repita a Password")]
        [DataType(DataType.Password)]
        public string ConfirmarPassword { get; set; }
    }
}
