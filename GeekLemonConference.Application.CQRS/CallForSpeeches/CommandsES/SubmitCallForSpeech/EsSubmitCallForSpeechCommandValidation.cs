using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.SubmitCallForSpeech
{
    public class EsSubmitCallForSpeechCommandValidation
        :
        AbstractValidator<EsSubmitCallForSpeechCommand>
    {
        public EsSubmitCallForSpeechCommandValidation()
        {
            RuleFor(c => c.Speaker.Name.First)
                .MinimumLength(2).MaximumLength(25)
                .WithMessage("{PropertName} Length is beewten 2 and 15");
            RuleFor(c => c.Speaker.Name.Last)
                .MinimumLength(2).MaximumLength(15)
                .WithMessage("{PropertName} Length is beewten 2 and 15");
        }
    }
}
