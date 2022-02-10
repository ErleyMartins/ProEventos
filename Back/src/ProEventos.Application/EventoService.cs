using System;
using System.Threading.Tasks;
using ProEventos.Application.Interface;
using ProEventos.Domain;
using ProEventos.Persistence.Interface;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geral;
        private readonly IEventoPersistence _evento;

        public EventoService(IGeralPersistence geral, IEventoPersistence evento)
        {
            _geral = geral;
            _evento = evento;

        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geral.Add<Evento>(model);
                if (await _geral.SaveChangesAsync())
                    return await _evento.GetEventoByIdAsync(model.Id);
                return null;
            }   
            catch (Exception ex)
            {
                throw new Exception($"Erro ao adicionar ao banco de dados. ERROR: {ex.Message}");
            }
        }

        public async Task<Evento> UpdateEventos(int id, Evento model)
        {
            try
            {
                var evento = await _evento.GetEventoByIdAsync(id);
                if (evento == null) return null;

                model.Id = evento.Id;

                _geral.Update(model);
                
                if (await _geral.SaveChangesAsync())
                        return await _evento.GetEventoByIdAsync(model.Id);
                
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

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _evento.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar todos os eventos. ERRO: {ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _evento.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar todos os eventos. ERRO: {ex.Message}");
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _evento.GetEventoByIdAsync(id, includePalestrantes);
                if (evento == null) return null;

                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o evento. ERRO: {ex.Message}");
            }
        }

    }
}