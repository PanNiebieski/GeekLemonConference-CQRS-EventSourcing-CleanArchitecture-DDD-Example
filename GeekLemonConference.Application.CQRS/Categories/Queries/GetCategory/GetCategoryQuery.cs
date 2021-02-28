using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<GetCategoryQueryResponse>
    {
        public QueryWitchDataBase queryWitchDataBase { get; set; }

        public CategoryId CategoryId { get; set; }

        public CategoryUniqueId CategoryUniqueId { get; set; }
    }
}
