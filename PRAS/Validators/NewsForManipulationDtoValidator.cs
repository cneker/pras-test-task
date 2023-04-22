using FluentValidation;
using PRAS.DataTransferObjects;

namespace PRAS.Validators
{
    public class NewsForManipulationDtoValidator : AbstractValidator<NewsForManipulationDto>
    {
        public NewsForManipulationDtoValidator()
        {
            RuleFor(n => n.Title)
                .NotEmpty();
            RuleFor(n => n.SubTitle)
                .NotEmpty();
            RuleFor(n => n.Description)
                .NotEmpty();
            RuleFor(n => n.FileName)
                .NotEmpty()
                .WithMessage("Select image");
        }
    }
}
