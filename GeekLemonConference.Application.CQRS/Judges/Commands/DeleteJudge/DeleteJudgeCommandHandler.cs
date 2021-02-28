using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Commands.DeleteJudge
{
    public class DeleteJudgeCommandHandler : IRequestHandler<DeleteJudgeCommand, DeleteJudgeCommandResponse>
    {
        private readonly IJudgeRepository _judgeRepository;
        private readonly IMapper _mapper;

        public DeleteJudgeCommandHandler(IJudgeRepository judgeRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _judgeRepository = judgeRepository;
        }

        public async Task<DeleteJudgeCommandResponse>
            Handle(DeleteJudgeCommand request, CancellationToken cancellationToken)
        {
            //var validator = new DeleteJudgeCommandValidator();
            //var validatorResult = await validator.ValidateAsync(request);

            //if (!validatorResult.IsValid)
            //    return new DeleteJudgeCommandResponse(validatorResult);
            ExecutionStatus databaseOperation;
            if (request.UniqueId != null)
                databaseOperation = await _judgeRepository.DeleteAsync(
                   request.UniqueId);
            else
                databaseOperation = await _judgeRepository.DeleteAsync(
                    request.Id);

            return new DeleteJudgeCommandResponse(databaseOperation);
        }


    }
}
