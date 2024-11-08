using CP3.Domain.Interfaces.Dtos;
using FluentValidation;


namespace CP3.Application.Dtos
{
    public class BarcoDto : IBarcoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public double Tamanho { get; set; }

        public void Validate()
        {
            var validationResult = new BarcoDtoValidation().Validate(this);

            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join(" e ", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }

    internal class BarcoDtoValidation : AbstractValidator<BarcoDto>
    {
        public BarcoDtoValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(x => $"O campo {nameof(x.Nome)} não pode ser vazio")
                .MinimumLength(3).WithMessage(x => $"O campo {nameof(x.Nome)} deve ter no mínimo 3 caracteres");

            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage(x => $"O campo {nameof(x.Modelo)} não pode ser vazio")
                .MinimumLength(3).WithMessage(x => $"O campo {nameof(x.Modelo)} deve ter no mínimo 3 caracteres");

            RuleFor(x => x.Ano)
                .GreaterThan(1900).WithMessage(x => $"O campo {nameof(x.Ano)} deve ser maior que 1900");

            RuleFor(x => x.Tamanho)
                .GreaterThan(0).WithMessage(x => $"O campo {nameof(x.Tamanho)} deve ser maior que 0");
        }
    }
}
