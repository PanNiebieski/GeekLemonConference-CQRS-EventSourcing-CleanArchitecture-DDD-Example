using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Categories;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemon.Infrastructure.Read.SQLite.Repositories
{
    public class ZEsCategoryRepository : CategoryRepository, IZEsCategoryRepository
    {


        public ZEsCategoryRepository(ICategoryAddDoer categoryAddDoer,
            ICategoryGetAllDoer categoryGetAllDoer, ICategoryDeleteDoer categoryDeleteDoer,
            ICategoryGetByIdDoer categoryGetByIdDoer, ICategoryUpdateDoer categoryUpdateDoer,
            IZEsGeekLemonDBContext zEsGeekLemonDBContext)
            : base(categoryAddDoer, categoryGetAllDoer, categoryDeleteDoer, categoryGetByIdDoer,
                 categoryUpdateDoer)

        {
            GeekLemonDBContext context =
                new GeekLemonDBContext(zEsGeekLemonDBContext.ConnectionString);

            this.ChangeContext(context);
        }


    }
}
