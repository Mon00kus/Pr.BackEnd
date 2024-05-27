using BackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Persistence.Context
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Cuestionario> Cuestionarios { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<RespuestaCuestionario> RespuestasCuestionarios { get; set; }
        public DbSet<RespuestaCuestionarioDetalle> RespuestasCuestionariosDetalles { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración adicional para evitar múltiples caminos en cascada
            modelBuilder.Entity<RespuestaCuestionarioDetalle>()
                .HasOne(r => r.RespuestaCuestionario)
                .WithMany(r => r.ListRespuestaCuestionarioDetalle)
                .HasForeignKey(r => r.RespuestaCuestionarioId)
                .OnDelete(DeleteBehavior.Cascade);  // Asumiendo que deseas mantener la eliminación en cascada aquí

            modelBuilder.Entity<RespuestaCuestionarioDetalle>()
                .HasOne(r => r.Respuesta)
                .WithMany()
                .HasForeignKey(r => r.RespuestaId)
                .OnDelete(DeleteBehavior.Restrict);  // Cambia a Restrict para evitar múltiples caminos de cascada
        }
    }
}