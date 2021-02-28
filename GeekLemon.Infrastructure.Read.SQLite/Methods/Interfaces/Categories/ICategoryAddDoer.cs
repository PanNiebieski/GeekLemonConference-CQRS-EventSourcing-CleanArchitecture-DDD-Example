using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Categories
{
    //public interface ICategoryAddDoer :  IAddDoer<Category, CategoryId, CategoryUniqueId>
    //{
    //}

    public interface ICategoryAddDoer : IBeforeDoer
    {
        Task<ExecutionStatus<CategoryIds>> Run(Category entity);
    }
}
