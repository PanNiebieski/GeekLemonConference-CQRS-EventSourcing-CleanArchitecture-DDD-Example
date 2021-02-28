using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Commands.UpdateJudge
{
    public class UpdateJudgeCommandHandler
          : IRequestHandler<UpdateJudgeCommand, UpdateJudgeCommandResponse>
    {
        private readonly IJudgeRepository _judgeRepository;
        private readonly IMapper _mapper;

        public UpdateJudgeCommandHandler(IJudgeRepository judgeRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _judgeRepository = judgeRepository;
        }

        public async Task<UpdateJudgeCommandResponse> Handle(UpdateJudgeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateJudgeCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new UpdateJudgeCommandResponse(validatorResult);

            var judgedto = _mapper.Map<JudgeDto>(request);
            var judge = _mapper.Map<Judge>(judgedto);

            ExecutionStatus statu;
            if (request.UniqueId != null)
                statu = await _judgeRepository.UpdateByUniqueIdAsync(judge);
            else
                statu = await _judgeRepository.UpdateByIdAsync(judge);

            if (statu.Success)
                return new UpdateJudgeCommandResponse();
            else
                return new UpdateJudgeCommandResponse(statu);

        }


    }
}
