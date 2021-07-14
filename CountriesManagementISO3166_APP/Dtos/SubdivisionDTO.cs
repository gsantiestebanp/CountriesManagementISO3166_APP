using FluentValidation;

namespace CountriesManagementISO3166_APP.Dtos
{
    public class SubdivisionDTO
    {
        public int SubdivisionId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string SubdivisionCode { get; set; }
    }

    public class SubdivisionDTOValidator : AbstractValidator<SubdivisionDTO>
    {
        public SubdivisionDTOValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("*Ingrese el nombre común de la subdivisión");
            RuleFor(x => x.SubdivisionCode).NotNull().NotEmpty().WithMessage("*Ingrese el código según norma ISO 3166-1 de la subdivisión");
        }
    }
}
