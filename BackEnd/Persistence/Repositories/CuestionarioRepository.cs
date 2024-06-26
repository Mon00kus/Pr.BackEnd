﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using BackEnd.Domain.IRepositories;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;

namespace BackEnd.Persistence.Repositories
{
    public class CuestionarioRepository: ICuestionarioRepository
    {
        private readonly AplicationDbContext _context;
        public CuestionarioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cuestionario> BuscarCuestionario(int idCuestionario, int idUsuario)
        {
            var cuestionario = await _context.Cuestionarios
                .Where(x=>x.Id==idCuestionario && x.Activo==1).FirstOrDefaultAsync();
            return cuestionario;
        }

        public async Task CreateCuestionario(Cuestionario cuestionario)
        {
            _context.Add(cuestionario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarCuestionario(Cuestionario cuestionario)
        {
            cuestionario.Activo = 0;
            _context.Entry(cuestionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Cuestionario> GetCuestionario(int idCuestionario)
        {
            var cuestionario = await _context.Cuestionarios
                .Where(x=> x.Activo==1 && x.Id==idCuestionario )
                .Include(x=> x.listPreguntas)
                .ThenInclude(x=> x.listRespuestas)
                .FirstOrDefaultAsync();
            return cuestionario;
        }

        public async Task<List<Cuestionario>> GetListCuestionarioByUser(int idUsuario)
        {
            var listCuestionario = await _context.Cuestionarios
                .Where(x => x.Activo==1 && x.UsuarioId==idUsuario).ToListAsync();
            return listCuestionario;
        }

        public Task<List<Cuestionario>> GetListCuestionario()
        {
            var listCuestionario = _context.Cuestionarios.Where(x => x.Activo == 1)
                .Select(o => new Cuestionario
                {
                    Id = o.Id,
                    Nombre = o.Nombre,
                    Descripcion = o.Descripcion,
                    FechaCreacion = o.FechaCreacion,
                    Usuario = new Usuario { NombreUsuario = o.Usuario.NombreUsuario}
                }).ToListAsync();
                //.Include(x => x.Usuario).ToListAsync();
            return listCuestionario;
        }
    }
}