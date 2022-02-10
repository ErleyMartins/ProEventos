using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interface;

namespace ProEventos.Persistence
{
    public class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext _context;
        public PalestrantePersistence(ProEventosContext context)
        {
            _context = context;
            // Comando para habilitar as no tracking geral
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(x => x.PalestranteEvento)
                    .ThenInclude(pe => pe.Evento);
            }

            query.OrderBy(x => x.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                .Include(x => x.RedesSociais);

            if (includeEventos)
                {
                    query = query
                        .Include(x => x.PalestranteEvento)
                        .ThenInclude(pe => pe.Evento);
                }

            query.OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }


        public async Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                .Include(x => x.RedesSociais);

            if (includeEventos)
                {
                    query = query
                        .Include(x => x.PalestranteEvento)
                        .ThenInclude(pe => pe.Evento);
                }

            query.Where(x => x.Id == id);

            return await query.FirstOrDefaultAsync();
        }

    }
}