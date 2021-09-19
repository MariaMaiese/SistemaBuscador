using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Models
{
    public class UsuarioCambioPasswordModel
    {
        public int Id { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(8, ErrorMessage = "El campo {0} debe tener como mínimo {1} caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La Contraseña debe tener al menos una mayúscula, minúsculas dígitos y 8 caracteres")]
        public string Password { get; set; }
        [Display(Name = "Repetir Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(8, ErrorMessage = "El campo {0} debe tener como mínimo {1} caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La Contraseña debe tener al menos una mayúscula, minúsculas dígitos y 8 caracteres")]
        [Compare("Password", ErrorMessage = "Las contraseñas deben ser iguales")]
        public string RePassword { get; set; }
    }
}
