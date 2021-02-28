using GeekLemonConference.Application.CQRS.Categories.Queries.GetAllCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.Categories.GetAllCategories
{
    public class GetCategoriesListQuery
         : IRequest<GetCategoriesListQueryResponse>
    {
        public QueryWitchDataBase queryWitchDataBase { get; set; }
    }
}
