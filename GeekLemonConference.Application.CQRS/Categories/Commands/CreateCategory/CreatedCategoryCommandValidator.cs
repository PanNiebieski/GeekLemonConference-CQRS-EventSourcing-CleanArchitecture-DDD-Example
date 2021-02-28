using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Commands.Categories.CreateCategory
{
    public class CreatedCategoryCommandValidator :
        AbstractValidator<CreatedCategoryCommand>
    {
        public CreatedCategoryCommandValidator()
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
