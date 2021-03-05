using AutoMapper;
using FluentValidation;
using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Categories.CommandsEs.UpdateCategory
{
    public class EsUpdateCategoryCommandHandler : IRequestHandler
        <EsUpdateCategoryCommand, EsUpdateCategoryCommandResponse>

    {
        private readonly ISessionForEventSourcing _sessionForEventSourcing;
        private readonly IMapper _mapper;

        public EsUpdateCategoryCommandHandler(
            ISessionForEventSourcing sessionForEventSourcing, IMapper mapper)
        {
            _mapper = mapper;
            _sessionForEventSourcing = sessionForEventSourcing;
        }


        public async Task<EsUpdateCategoryCommandResponse> Handle
            (EsUpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new EsUpdateCategoryCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new EsUpdateCategoryCommandResponse(validatorResult);

            var category = _mapper.Map<Category>(request);

            var eventstoreResult = Get<CategoryAggregate>
                (category.UniqueId.GetAggregateKey());

            if (!eventstoreResult.Success)
                return new EsUpdateCategoryCommandResponse
                    (eventstoreResult.RemoveGeneric());

            if ((eventstoreResult.Value.Version - 1) > (category.Version))
                return new EsUpdateCategoryCommandResponse
                    (ExecutionStatus.EventStoreConcurrencyError(@$"You sended old version.
                    Yours {category.Version}. Should be:{eventstoreResult.Value.Version}"));

            eventstoreResult.Value.Update(category);

            var status = _sessionForEventSourcing.Commit();

            return new EsUpdateCategoryCommandResponse(status);
        }

        private ExecutionStatus<T> Get<T>(AggregateKey id, int? expectedVersion = null) where T : AggregateRoot
        {
            var a = _sessionForEventSourcing.Get<T>(id, expectedVersion);
            return a;
        }
    }
}
