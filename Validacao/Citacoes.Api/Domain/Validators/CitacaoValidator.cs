using FluentValidation;

namespace Citacoes.Api.Domain.Validators
{
    public class CitacaoValidator : AbstractValidator<Citacao>
    {
        public CitacaoValidator()
        {
            RuleFor(x => x.Texto)
                .NotEmpty().WithMessage("O preenchimento do Texto é obrigatório")
                .MinimumLength(3).WithMessage("O texto deve conter no mínimo 3 caracteres")
                .NotEqual(x => x.Autor).WithMessage("O Texto deve ser diferente do Autor");

            RuleFor(x => x.Autor)
                .NotEmpty().WithMessage("O preenchimento do autor é obrigatório")
                .Length(3, 200).WithMessage("O autor deve conter entre 3 e 200 caracteres")
                .NotEqual(x => x.Texto).WithMessage("O Autor deve ser diferente do Texto");
        }
    }
}
