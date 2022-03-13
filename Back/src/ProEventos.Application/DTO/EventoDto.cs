using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.DTO
{
    public class EventoDto
    {
        public int Id { get; set; }
        
        public string Local { get; set; }
        
        public string DataEvento { get; set; }

        [
            Required(ErrorMessage = "O campo {0} é obrigatório."),
            //MinLength(3, ErrorMessage = "O campo {0} deve ter no mínimo 3 caracteres."),
            //MaxLength(50, ErrorMessage = "O campo {0} deve ter no maxímo 50 caracteres."),
            StringLength(50, MinimumLength = 3)
        ]
        public string Tema { get; set; }

        [
            Display(Name = "Quantidade de pessoas"),
            Required(ErrorMessage = "O campo {0} é obrigatorio."),
            Range(1, 120000, ErrorMessage = "O {0} deve ser de 1 a 120.000")
        ]
        public int QtdPessoa  { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem valida. Tipos permitidos: gif, jpeg, jpg, bmp e png")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [Phone(ErrorMessage = "O campo {0} esta com número inválido.")]
        public string Telefone { get; set; }

        [Required()]
        [Display(Name = "e-mail")]
        [EmailAddress(ErrorMessage = "O campo {0} precisa de um e-mail valido.!")]
        public string Email { get; set; }

        public IEnumerable<LoteDto> Lotes { get; set; }

        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }

        public IEnumerable<PalestranteDto> Palestrante { get; set; }

    }
}