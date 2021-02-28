using FluentValidation;
using GeekLemonConference.Application.CQRS.Categories.Commands.UpdateCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Categories.CommandsEs.UpdateCategory
{
    public class EsUpdateCategoryCommandValidator :
    AbstractValidator<EsUpdateCategoryCommand>
    {
        public EsUpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .MinimumLength(2).MaximumLength(15)
                .WithMessage("{PropertName} Length is beewten 2 and 15");
            RuleFor(c => c.DisplayName)
                .MinimumLength(2).MaximumLength(15)
                .WithMessage("{PropertName} Length is beewten 2 and 15");
        }
    }
}
