using BackEnd.Domain.IRepositories;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Persistence.Repositories
{
    public class RespuestaCuestionarioRepository : IRespuestaCuestionarioRepository
    {
        private readonly AplicationDbContext _context;

        public RespuestaCuestionarioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            respuestaCuestionario.Activo = 1;
            respuestaCuestionario.Fecha = DateTime.Now;
            _context.Add(respuestaCuestionario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RespuestaCuestionario>> ListRespuestaCuestionario(int idCuestionario, int idUsuario)
        {
            var listRespCuestionario = await _context.RespuestasCuestionarios.Where(rq=>rq.Id == idCuestionario && 
                rq.Activo == 1 &&
                rq.Cuestionario.UsuarioId ==  idUsuario).OrderByDescending(rq=> rq.Fecha).ToListAsync();

            return listRespCuestionario;
        }

        public async Task<RespuestaCuestionario> BuscarRespuestaCuestionario(int idRtaCuestionario, int idUsuario)
        {
            var respCuestionario = await _context.RespuestasCuestionarios.Where(rq=> rq.Id==idRtaCuestionario &&
                rq.Cuestionario.UsuarioId == idUsuario &&
                rq.Activo == 1).FirstOrDefaultAsync();

            return respCuestionario;
        }

        public async Task EliminarRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            respuestaCuestionario.Activo = 0;
            _context.Entry(respuestaCuestionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetIdCuestionarioByIdRespuesta(int idRespuestaCuestionario)
        {
            var cuestionario = await _context.RespuestasCuestionarios.Where(rq => rq.Id == idRespuestaCuestionario
                                                                        && rq.Activo == 1).FirstOrDefaultAsync();
            return cuestionario.CuestionarioId;
        }

        public async Task<List<RespuestaCuestionarioDetalle>> GetListRespuestas(int idRespuestaCuestionario)
        {
            var listRespuestas = await _context.RespuestasCuestionariosDetalles.Where(rq => rq.RespuestaCuestionarioId == idRespuestaCuestionario).Select(rq => new RespuestaCuestionarioDetalle{ RespuestaId = rq.RespuestaId}).ToListAsync();
            return listRespuestas;
        }
    }
}