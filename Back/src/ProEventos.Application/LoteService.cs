using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.DTO;
using ProEventos.Application.Interface;
using ProEventos.Domain;
using ProEventos.Persistence.Interface;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersistence _geral;
        private readonly ILotePersistence _lote;
        private readonly IMapper _mapper;
        public LoteService(IGeralPersistence geral, ILotePersistence lote, IMapper mapper)
        {
            _geral = geral;
            _lote = lote;
            _mapper = mapper;
        }

        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;

                _geral.Add<Lote>(lote);

                await _geral.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateLote(int eventoId, LoteDto model, Lote[] lotes)
        {
            try
            {
                var lote = lotes.FirstOrDefault(x => x.Id == model.Id);
                model.EventoId = eventoId;

                _mapper.Map(model, lote);
                _geral.Update<Lote>(lote);

                await _geral.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int id)
        {
            try
            {
                var lote = await _lote.GetLoteByIdsAsync(eventoId, id);
                if (lote == null) return null;

                var resultado = _mapper.Map<LoteDto>(lote);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lote.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                var resultado = _mapper.Map<LoteDto[]>(lotes);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int id)
        {
            try
            {
                var lote = await _lote.GetLoteByIdsAsync(eventoId, id);
                if (lote == null) throw new Exception("Não foi encontrado lote para ser deletado.");

                _geral.Delete<Lote>(lote);

                return await _geral.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await _lote.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0) await AddLote(eventoId, model);
                    else await UpdateLote(eventoId, model, lotes);
                }

                var loteRetorno = await _lote.GetLotesByEventoIdAsync(eventoId);
                return _mapper.Map<LoteDto[]>(loteRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}