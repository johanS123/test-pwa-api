using System.ComponentModel.DataAnnotations;

namespace testLoggers.Models.ViewModel
{
    public class UsersVM
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        
        [Required(ErrorMessage = "El correo no puede estar vacio.")]
        public string email { get; set; }

        public string role { get; set; }
    }
}
