using AutoMapper;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Application.Contracts.Persistence.WithES;

namespace GeekLemonConference.Application.CQRS.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryQueryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IZEsCategoryRepository _zEscategoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IMapper mapper,
            IZEsCategoryRepository zEscategoryRepository,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _zEscategoryRepository = zEscategoryRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<GetCategoryQueryResponse> Handle(GetCategoryQuery request,
            CancellationToken cancellationToken)
        {
            ExecutionStatus<Category> databaseOperation = null;

            if (request.CategoryUniqueId != null)
                if (request.queryWitchDataBase == QueryWitchDataBase.WithEventSourcing)
                    databaseOperation = await _zEscategoryRepository.GetByIdAsync(request.CategoryUniqueId);
                else
                    databaseOperation = await _categoryRepository.GetByIdAsync(request.CategoryUniqueId);
            else
                if (request.queryWitchDataBase == QueryWitchDataBase.WithEventSourcing)
                databaseOperation = await _zEscategoryRepository.GetByIdAsync(request.CategoryId);
            else
                databaseOperation = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (!databaseOperation.Success)
                return new GetCategoryQueryResponse(databaseOperation.RemoveGeneric());

            var categorydto = _mapper.Map<CategoryDto>(databaseOperation.Value);

            return new GetCategoryQueryResponse(categorydto);
        }
    }
}
