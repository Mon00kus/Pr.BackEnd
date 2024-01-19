﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace BackEnd.Domains.Models
{
    public class Cuestonario
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Activo { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set;}
        public List<Pregunta> listPreguntas { get; set; }
    }
}
