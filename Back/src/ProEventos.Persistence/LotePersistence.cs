using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interface;

namespace ProEventos.Persistence
{
    public class LotePersistence : ILotePersistence
    {
        private readonly ProEventosContext _context;
        public LotePersistence(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = _context.Lotes
                .AsNoTracking()
                .Where(x => x.EventoId == eventoId && x.Id == id);

            return await query.FirstOrDefaultAsync();                
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes
                .AsNoTracking()
                .Where(x => x.EventoId == eventoId)
                .OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }
    }
}