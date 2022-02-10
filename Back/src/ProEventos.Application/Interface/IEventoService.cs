using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Interface
{
    public interface IEventoService
    {
        Task<Evento> AddEventos(Evento model);
        Task<Evento> UpdateEventos(int id, Evento model);
        Task<bool> DeleteEvento(int id);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false);
    }
}