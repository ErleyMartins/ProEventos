using System.Threading.Tasks;
using ProEventos.Application.DTO;

namespace ProEventos.Application.Interface
{
    public interface ILoteService
    {
        Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models);
        Task<bool> DeleteLote(int eventoId, int id);
        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDto> GetLoteByIdsAsync(int eventoId, int id);
    }
}