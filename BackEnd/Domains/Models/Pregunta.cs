using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Domains.Models
{
    public class Pregunta
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Descripcion { get; set; }
        public int CuestionarioId { get; set; }
        public Cuestonario Cuestonario { get; set; } = new Cuestonario();
        public List<Respuesta> listRespuestas { get; set; }
    }
}
