using System.ComponentModel.DataAnnotations;

namespace testLoggers.Models.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string user { get; set; }
        [Required(ErrorMessage = "La clave es obligatoria.")]
        public string password { get; set; }
    }
}
