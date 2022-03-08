using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.DTO;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. ERRO: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)    
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (System.Exception ex)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento. ERRO: {ex.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. ERRO: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = await _eventoService.AddEventos(model);
                if (evento == null) return BadRequest(new { message = "Não foi possível adicionar o evento!" });

                return Ok(evento);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar um novo evento. ERRO: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {
            try
            {
                var evento = await _eventoService.UpdateEventos(id, model);
                if (evento == null) return BadRequest(new { message = "Não foi possível atualizar o evento!" });

                return Ok(evento);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar o evento. ERRO: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _eventoService.DeleteEvento(id)) 
                    return Ok(new { message = $"Evento do id: {id} foi deletado com sucesso!" });
                else
                    return BadRequest(new { message = "Não foi possivel deletar o evento!" });

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o evento. ERRO: {ex.Message}");
            }
        }

    }
}
