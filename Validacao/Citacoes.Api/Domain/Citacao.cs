using System.ComponentModel.DataAnnotations;

namespace Citacoes.Api.Domain
{
    public class Citacao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O preenchimento do Texto é obrigatório")]
        [MinLength(3, ErrorMessage = "O texto deve conter no mínimo 3 caracteres")]
        public string Texto { get; set; }

        [Required(ErrorMessage = "O preenchimento do autor é obrigatório")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O autor deve conter entre 3 e 200 caracteres")]
        public string Autor { get; set; }
    }
}
