namespace ApiRest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Idcita { get; set; }

        public int? Idpaciente { get; set; }

        public int? IdDoctor { get; set; }

        [StringLength(50)]
        public string Fecha { get; set; }
    }
}
