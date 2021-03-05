using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.CallForSpeeches.Command.SubmitCallForSpeech
{
    public class SubmitCallForSpeechCommandHandler :
        IRequestHandler<SubmitCallForSpeechCommand, SubmitCallForSpeechCommandResponse>
    {
        private readonly ICallForSpeechRepository _callRepository;
        private readonly IMapper _mapper;

        public SubmitCallForSpeechCommandHandler(ICallForSpeechRepository callRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _callRepository = callRepository;
        }


        public async Task<SubmitCallForSpeechCommandResponse>
            Handle(SubmitCallForSpeechCommand request, CancellationToken cancellationToken)
        {
            var validator = new SubmitCallForSpeechCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new SubmitCallForSpeechCommandResponse(validatorResult);

            var cfs = _mapper.Map<CallForSpeech>(request);

            var id = await _callRepository.SubmitAsync(cfs);

            if (!id.Success)
                return new SubmitCallForSpeechCommandResponse();

            return new SubmitCallForSpeechCommandResponse(_mapper.Map<IdsDto>(id.Value));


        }




    }
}
