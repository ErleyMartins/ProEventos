using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interface
{
    public interface IPalestrantePersistence
    {
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEventos);
    }
}