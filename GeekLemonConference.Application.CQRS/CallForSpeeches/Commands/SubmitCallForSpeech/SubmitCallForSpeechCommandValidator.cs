﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.SubmitCallForSpeech
{
    public class SubmitCallForSpeechCommandValidator :
    AbstractValidator<SubmitCallForSpeechCommand>
    {
        public SubmitCallForSpeechCommandValidator()
        {
            //RuleFor(c => c.Login)
            //    .MinimumLength(2).MaximumLength(15)
            //    .WithMessage("{PropertName} Length is beewten 2 and 15");

            //RuleFor(c => c.Name.First)
            //    .MinimumLength(2)
            //    .WithMessage("{PropertName} Length is beewten 2 ");

            //RuleFor(c => c.Name.Last)
            //    .MinimumLength(2)
            //    .WithMessage("{PropertName} Length is beewten 2 ");
        }
    }

}
