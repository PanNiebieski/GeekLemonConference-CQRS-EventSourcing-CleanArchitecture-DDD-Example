using AutoMapper;
using GeekLemon.Infrastructure.Read.SQLite.Repositories;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.DomainEvents.Categories;
using GeekLemonConference.DomainEvents.Ddd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers.Categories
{
    public class SubscribeCreatedCategory : SubscribeBase
    {
        private IZEsCategoryRepository _zEsCategoryRepository;
        private IMapper _mapper;

        public SubscribeCreatedCategory(IZEsCategoryRepository ZEsCategoryRepository,
            IMapper mapper)
        {
            _zEsCategoryRepository = ZEsCategoryRepository;
            _mapper = mapper;
        }

        public override string QUEUE_Name => Constants.QUEUE_CATEGORY_CREATED;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<CategoryCreatedEvent>(json);
        }

        public async override Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            CategoryCreatedEvent categoryCreateEvent = @event as CategoryCreatedEvent;

            var category = _mapper.Map<Category>(categoryCreateEvent);

            var execution = await _zEsCategoryRepository.AddAsync(category);

            return execution.RemoveGeneric();
        }
    }
}
