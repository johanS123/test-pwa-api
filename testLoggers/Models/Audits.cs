using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testLoggers.Models
{
    public class Audits
    {
        public DateTime datelog { get; set; }

        [Key]
        public int idUser { get; set; }

        [ForeignKey("idUser")]
        public virtual Users Users { get; set; }

    }
}
