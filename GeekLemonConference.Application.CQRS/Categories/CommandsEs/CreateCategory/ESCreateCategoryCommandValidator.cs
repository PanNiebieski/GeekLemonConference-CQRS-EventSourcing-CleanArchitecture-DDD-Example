using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Categories.CommandsEs.CreateCategory
{
    public class ESCreateCategoryCommandValidator :
        AbstractValidator<ESCreateCategoryCommand>
    {
        public ESCreateCategoryCommandValidator()
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
