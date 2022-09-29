using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace testLoggers.Models
{
    public class Users
    {

        public Users()
        {
            Audits = new HashSet<Audits>();
        }

        [Key]
        public int id { get; set; }
        [StringLength(50)]
        public string firstname { get; set; }
        [StringLength(50)]
        public string lastname { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage="El correo no puede estar vacio.")]
        public string email { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "La contrasela no puede estar vacia.")]
        public string password { get; set; }

        [Compare("password", ErrorMessage = "Las contraseñas no coinciden.")]
        [NotMapped]
        public string confirmPassword { get; set; }
        
        [AllowNull]
        public string? salt { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        public virtual ICollection<Audits> Audits { get; set; }

    }
}
