using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.DomainEvents.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Commands.Categories.CreateCategory
{
    public class CreatedCategoryCommandHandler
        : IRequestHandler<CreatedCategoryCommand, CreatedCategoryCommandResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreatedCategoryCommandHandler(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CreatedCategoryCommandResponse> Handle
            (CreatedCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatedCategoryCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new CreatedCategoryCommandResponse(validatorResult);

            var category = _mapper.Map<Category>(request);
            category.Version = category.Version + 1;
            var databaseResult = await _categoryRepository.AddAsync(category);

            if (!databaseResult.Success)
                return new CreatedCategoryCommandResponse(databaseResult.RemoveGeneric());

            var Idsdto = _mapper.Map<IdsDto>(databaseResult.Value);
            return new CreatedCategoryCommandResponse(Idsdto);
        }
    }
}
