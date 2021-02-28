using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Commands.CreateJudge
{
    public class CreateJudgeCommandHandler
              : IRequestHandler<CreateJudgeCommand, CreateJudgeCommandResponse>
    {
        private readonly IJudgeRepository _judgeRepository;
        private readonly IMapper _mapper;


        public CreateJudgeCommandHandler(IJudgeRepository judgeRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _judgeRepository = judgeRepository;
        }


        public async Task<CreateJudgeCommandResponse>
            Handle(CreateJudgeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateJudgeCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new CreateJudgeCommandResponse(validatorResult);

            var judge = _mapper.Map<Judge>(request);

            var databaseoperation = await _judgeRepository.AddAsync(judge);

            if (!databaseoperation.Success)
                return new CreateJudgeCommandResponse
                    (databaseoperation.RemoveGeneric());

            var Idsdto = _mapper.Map<IdsDto>(databaseoperation.Value);
            return new CreateJudgeCommandResponse(Idsdto);
        }



    }
}
