using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BackEnd.Domains.Models;



namespace BackEnd.Persistence.Context
{
    public class AplicationDbContext : DbContext
    {

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cuestonario> Cuestionario { get; set; }
        public DbSet<Pregunta> Pregunta { get; set; }
        public DbSet<Respuesta> Respuesta { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}
