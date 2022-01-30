using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Evento> _evento = new Evento[]
        {
            new Evento() {
                EventoID = 1,
                Tema = "Angular e .NET Core 5",
                Local = "São Paulo",
                Lote = "1º Lote",
                DataEvento = DateTime.Now.AddDays(5).ToString("dd/MM/yyyy"),
                QtdPessoa = 250,
                ImagemURL = "Foto.png"
            },
            new Evento() {
                EventoID = 2,
                Tema = "Angular e suas novidades",
                Local = "São Paulo",
                Lote = "2º Lote",
                DataEvento = DateTime.Now.AddDays(15).ToString("dd/MM/yyyy"),
                QtdPessoa = 230,
                ImagemURL = "Foto.png"
            }
        };

        public EventoController()
        {

        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return _evento.Where(evento => evento.EventoID == id);
        }

    }
}
