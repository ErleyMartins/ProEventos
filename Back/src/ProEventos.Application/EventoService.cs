using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.DTO;
using ProEventos.Application.Interface;
using ProEventos.Domain;
using ProEventos.Persistence.Interface;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geral;
        private readonly IEventoPersistence _evento;
        private readonly IMapper _mapper;
        public EventoService(IGeralPersistence geral, IEventoPersistence evento, IMapper mapper)
        {
            _geral = geral;
            _evento = evento;
            _mapper = mapper;
        }
        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                _geral.Add<Evento>(evento);
                if (await _geral.SaveChangesAsync())
                {
                    var retorno = await _evento.GetEventoByIdAsync(evento.Id);
                    return _mapper.Map<EventoDto>(retorno);
                }
                return null;
            }   
            catch (Exception ex)
            {
                throw new Exception($"Erro ao adicionar ao banco de dados. ERROR: {ex.Message}");
            }
        }

        public async Task<EventoDto> UpdateEventos(int id, EventoDto model)
        {
            try
            {
                var evento = await _evento.GetEventoByIdAsync(id);
                if (evento == null) return null;

                _mapper.Map(model, evento);
                model.Id = evento.Id;

                _geral.Update<Evento>(evento);
                
                if (await _geral.SaveChangesAsync())
                {
                    var resultado = await _evento.GetEventoByIdAsync(evento.Id);
                    return _mapper.Map<EventoDto>(evento);
                }
                
                return null;
            }
            catch (Exception ex)
            {
              throw new Exception($"Erro ao atualizar o evento. ERROR: {ex.Message}");
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            try
            {
                var evento = await _evento.GetEventoByIdAsync(id);
                if (evento == null) throw new Exception("Evento não encontrado!");

                _geral.Delete<Evento>(evento);
                return await _geral.SaveChangesAsync();
            }
            catch (Exception ex)
            {
              throw new Exception($"Erro ao atualizar o evento. ERROR: {ex.Message}");
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _evento.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar todos os eventos. ERRO: {ex.Message}");
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _evento.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar todos os eventos. ERRO: {ex.Message}");
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int id, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _evento.GetEventoByIdAsync(id, includePalestrantes);
                if (evento == null) return null;

                var resultado = _mapper.Map<EventoDto>(evento);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o evento. ERRO: {ex.Message}");
            }
        }

    }
}