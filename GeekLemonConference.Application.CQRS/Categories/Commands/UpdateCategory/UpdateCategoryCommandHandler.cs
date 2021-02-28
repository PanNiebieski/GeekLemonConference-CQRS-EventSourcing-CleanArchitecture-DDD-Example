using AutoMapper;
using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.DomainEvents.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler
        <UpdateCategoryCommand, UpdateCategoryCommandResponse>
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<UpdateCategoryCommandResponse> Handle
            (UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCategoryCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new UpdateCategoryCommandResponse(validatorResult);

            var catdtoegory = _mapper.Map<CategoryDto>(request);
            var category = _mapper.Map<Category>(catdtoegory);

            //category.Version = category.Version + 1;

            ExecutionStatus result;
            if (request.UniqueId != null)
                result = await _categoryRepository.UpdateByUniqueIdAsync(category);
            else
                result = await _categoryRepository.UpdateByIdAsync(category);
            return new UpdateCategoryCommandResponse(result);
        }



    }
}
