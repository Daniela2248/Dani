namespace ApiRest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Paciente")]
    public partial class Paciente
    {
        [Key]
        public int IdPaciente { get; set; }

        [StringLength(50)]
        public string APpaterno { get; set; }

        [StringLength(50)]
        public string APmaterno { get; set; }

        [StringLength(50)]
        public string Nombres { get; set; }
    }
}
