using FluentValidation;
using MovieStoreB.Controllers;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Validators
{
    public class TestRequestValidator : AbstractValidator<Movie>
    {
        public TestRequestValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Въведи по-голямо от 0");
            //RuleFor(x => x.Title).NotNull().NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Year).GreaterThan(0).WithMessage("Въведи по-голямо от 0");
            RuleFor(x => x.Actors).NotNull().NotEmpty();
        }
    }
}
