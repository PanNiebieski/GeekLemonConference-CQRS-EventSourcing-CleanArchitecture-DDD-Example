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
    public class SubscribeUpdateCategory : SubscribeBase
    {
        private IZEsCategoryRepository _zEsCategoryRepository;
        private IMapper _mapper;

        public SubscribeUpdateCategory(IZEsCategoryRepository ZEsCategoryRepository,
            IMapper mapper)
        {
            _zEsCategoryRepository = ZEsCategoryRepository;
            _mapper = mapper;
        }

        public override string QUEUE_Name => Constants.QUEUE_CATEGORY_UPDATED;

        public override DomainEvent DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<CategoryUpdateEvent>(json);
        }

        public override async Task<ExecutionStatus> HandleEvent(DomainEvent @event)
        {
            CategoryUpdateEvent categoryCreateEvent = @event as CategoryUpdateEvent;

            var category = _mapper.Map<Category>(categoryCreateEvent);

            var execution = await
                _zEsCategoryRepository.UpdateByUniqueIdAsync(category);

            return execution;
        }
    }
}
