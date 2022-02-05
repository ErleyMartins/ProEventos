using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interface;

namespace ProEventos.Persistence
{
    public class EventoPersistence : IEventoPersistence
    {
        private readonly ProEventosContext _context;
        public EventoPersistence(ProEventosContext context)
        {
            _context = context;
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestranteEvento)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query.OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

                if (includePalestrantes)
                {
                    query = query
                        .Include(x => x.PalestranteEvento)
                        .ThenInclude(pe => pe.Palestrante);
                }
                
                query.OrderBy(x => x.Tema).Where(x => x.Tema.ToLower().Contains(tema.ToLower()));

                return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

                if (includePalestrantes)
                {
                    query = query
                        .Include(x => x.PalestranteEvento)
                        .ThenInclude(pe => pe.Palestrante);
                }
                
                query.Where(x => x.Id == id);

                return await query.FirstOrDefaultAsync();
        }

    }
}