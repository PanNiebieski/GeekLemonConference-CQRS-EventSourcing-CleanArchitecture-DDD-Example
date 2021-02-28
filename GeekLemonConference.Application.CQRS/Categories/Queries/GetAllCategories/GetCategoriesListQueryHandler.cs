using AutoMapper;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.Categories.Queries.GetAllCategories;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.Categories.GetAllCategories
{
    public class GetCategoriesListQueryHandler :
        IRequestHandler<GetCategoriesListQuery, GetCategoriesListQueryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IZEsCategoryRepository _zEscategoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandler(IMapper mapper,
            ICategoryRepository categoryRepository,
            IZEsCategoryRepository zEscategoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _zEscategoryRepository = zEscategoryRepository;
        }


        public async Task<GetCategoriesListQueryResponse> Handle
            (GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            ExecutionStatus<IReadOnlyList<Category>> databaseOperation = null;

            if (request.queryWitchDataBase == QueryWitchDataBase.WithEventSourcing)
                databaseOperation = await _zEscategoryRepository.GetAllAsync();
            else
                databaseOperation = await _categoryRepository.GetAllAsync();

            if (!databaseOperation.Success)
                return new GetCategoriesListQueryResponse(databaseOperation.RemoveGeneric());

            var ordered = databaseOperation.Value.OrderBy(a => a.Name);
            var mpaed = _mapper.Map<List<CategoryInListViewModel>>(ordered);
            return new GetCategoriesListQueryResponse(mpaed);
        }
    }
}
