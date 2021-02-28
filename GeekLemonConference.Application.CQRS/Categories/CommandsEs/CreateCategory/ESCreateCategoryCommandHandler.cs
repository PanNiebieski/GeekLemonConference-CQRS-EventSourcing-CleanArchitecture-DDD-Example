using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Commands.Categories.CreateCategory;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Categories.CommandsEs.CreateCategory
{
    public class ESCreateCategoryCommandHandler
         : IRequestHandler<ESCreateCategoryCommand, ESCreateCategoryCommandResponse>
    {
        private readonly ISessionForEventSourcing _sessionForEventSourcing;
        private readonly IMapper _mapper;

        public ESCreateCategoryCommandHandler(
            ISessionForEventSourcing sessionForEventSourcing, IMapper mapper)
        {
            _mapper = mapper;
            _sessionForEventSourcing = sessionForEventSourcing;
        }

        public async Task<ESCreateCategoryCommandResponse> Handle
            (ESCreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new ESCreateCategoryCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new ESCreateCategoryCommandResponse(validatorResult);

            var category = _mapper.Map<Category>(request);

            var item = new CategoryAggregate(category);

            _sessionForEventSourcing.Add<CategoryAggregate>(item);
            _sessionForEventSourcing.Commit();

            var ids = _mapper.Map<IdsDto>(category.Ids());
            return new ESCreateCategoryCommandResponse(ids);
        }

    }
}
