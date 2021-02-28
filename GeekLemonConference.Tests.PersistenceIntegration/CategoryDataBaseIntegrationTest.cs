using AutoMapper;
using Dapper;
using FluentAssertions;
using GeekLemon.Infrastructure.Read.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.Map;
using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Tests.Builders;
using Microsoft.Data.Sqlite;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
//using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using static GeekLemonConference.Tests.Builders.CategoryBuilder;

namespace GeekLemonConference.IntegrationTests.Persistence
{
    public class CategoryDataBaseIntegrationTest : IDisposable
    {
        private GeekLemonDBContext _geekLemonContex;
        private IMapper _mapper;
        private string _tempdatabasefile;

        public CategoryDataBaseIntegrationTest()
        {
            _tempdatabasefile = CopyDataBase.Run();

            if (_geekLemonContex == null)
                _geekLemonContex =
                    new GeekLemonDBContext(
                        $"Data Source={_tempdatabasefile}");

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        public void Dispose()
        {
            //try
            //{
            //    File.Delete(_tempdatabasefile);
            //    Directory.Delete(_tempdatabasefile.Replace("\\TEMPCOPY{i}.db", ""));
            //}
            //catch (Exception
            //ex)
            //{

            //}
        }


        [Fact]
        public async Task CreateCategoryShouldBeSuccessThenGetByIdShouldbeAlsoASuccess()
        {
            //SetupCategories();
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat = GivenCategory().WithId(1).WithName("AAA")
                .Build();

            var status = await categoryAddDoer.Run(cat);

            status.Success.Should().BeTrue();

            CategoryGetByIdDoer categoryGetByIdDoer =
                new CategoryGetByIdDoer(_geekLemonContex, _mapper);

            var status2 = await categoryGetByIdDoer.Run(
                status.Value.CreatedId as CategoryId);

            status2.Success.Should().BeTrue();

            status2.Value.Should().Equals(cat);
        }

        [Fact]
        public async Task CreateCategoryShouldBeSuccessThenGetByUniqueIdShouldbeAlsoASuccess()
        {
            //SetupCategories();
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat = GivenCategory().WithId(1).WithName("AAA")
                .Build();

            var status = await categoryAddDoer.Run(cat);

            status.Success.Should().BeTrue();

            CategoryGetByIdDoer categoryGetByIdDoer =
                new CategoryGetByIdDoer(_geekLemonContex, _mapper);

            var status2 = await categoryGetByIdDoer.Run(
                status.Value.UniqueId as CategoryUniqueId);

            status2.Success.Should().BeTrue();

            status2.Value.Should().Equals(cat);
        }

        [Fact]
        public async Task CreateCategoryShouldBeSuccessThenGetAllShouldbeAlsoASuccess()
        {
            //SetupCategories();
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat = GivenCategory().WithId(1).WithName("AAA")
                .Build();

            var status = await categoryAddDoer.Run(cat);

            status.Success.Should().BeTrue();

            CategoryGetAllDoer categoryGetAllDoer =
                new CategoryGetAllDoer(_geekLemonContex, _mapper);

            var status2 = await categoryGetAllDoer.Run();

            status2.Success.Should().BeTrue();

            status2.Value.Should().HaveCount(1);
        }

        [Fact]
        public async Task CreateCategoryShouldBeSuccessThenDeleteThenTryGetByIdAndFail()
        {
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat = GivenCategory().WithId(1).WithName("AAA")
                .Build();

            var status = await categoryAddDoer.Run(cat);

            status.Success.Should().BeTrue();

            CategoryDeleteDoer categoryDeleteDoer =
                new CategoryDeleteDoer(_geekLemonContex, _mapper);

            var status2 = await categoryDeleteDoer.Run(
                status.Value.CreatedId as CategoryId);

            status2.Success.Should().BeTrue();

            CategoryGetByIdDoer categoryGetByIdDoer =
                new CategoryGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await categoryGetByIdDoer.Run(
                status.Value.CreatedId as CategoryId);

            status3.Success.Should().BeFalse();
        }

        [Fact]
        public async Task CreateCategoryShouldBeSuccessThenDeleteByUniqueIdThenTryGetByUniqueIdAndFail()
        {
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat = GivenCategory().WithId(1).WithName("AAA")
                .Build();

            var status = await categoryAddDoer.Run(cat);

            status.Success.Should().BeTrue();

            CategoryDeleteDoer categoryDeleteDoer =
                new CategoryDeleteDoer(_geekLemonContex, _mapper);

            var status2 = await categoryDeleteDoer.Run(
                status.Value.UniqueId as CategoryUniqueId);

            status2.Success.Should().BeTrue();

            CategoryGetByIdDoer categoryGetByIdDoer =
                new CategoryGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await categoryGetByIdDoer.Run(
                status.Value.UniqueId as CategoryUniqueId);

            status3.Success.Should().BeFalse();
        }


        [Fact]
        public async Task CreateCategoryThenUpdatedShoudlBeSuccessById()
        {
            //SetupCategories();
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat = GivenCategory().WithId(1).WithName("AAA")
                .Build();

            var status = await categoryAddDoer.Run(cat);

            status.Success.Should().BeTrue();

            CategoryUpdateDoer categoryUpdateDoer =
                new CategoryUpdateDoer(_geekLemonContex, _mapper);

            var cat2 = GivenCategory().WithId(1).WithName("2222")
                .WithDisplayName("3333")
                .WithWhatWeAreLookingFor("4444")
                .Build();

            var status2 = await categoryUpdateDoer.Run(cat2, ByWhatId.CreatedId);
            status2.Success.Should().BeTrue();

            CategoryGetByIdDoer categoryGetByIdDoer =
                new CategoryGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await categoryGetByIdDoer.Run(
                status.Value.UniqueId as CategoryUniqueId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(cat);
        }

        [Fact]
        public async Task CreateCategoryThenUpdatedShoudlBeSuccessByUniqueId()
        {
            //SetupCategories();
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat = GivenCategory().WithId(1).WithName("AAA")
                .Build();

            var status = await categoryAddDoer.Run(cat);

            status.Success.Should().BeTrue();

            CategoryGetByIdDoer categoryGetByIdDoer =
                new CategoryGetByIdDoer(_geekLemonContex, _mapper);

            var status2 = await categoryGetByIdDoer.Run(
                status.Value.UniqueId as CategoryUniqueId);

            CategoryUpdateDoer categoryUpdateDoer =
                new CategoryUpdateDoer(_geekLemonContex, _mapper);

            var cat2 = GivenCategory().WithId(1).WithName("2222")
                .WithDisplayName("3333")
                .WithWhatWeAreLookingFor("4444")
                .Build();

            var status3 = await categoryUpdateDoer.Run(cat2, ByWhatId.UniqueId);
            status3.Success.Should().BeTrue();

            var status4 = await categoryGetByIdDoer.Run(
                status.Value.UniqueId as CategoryUniqueId);

            status3.Success.Should().BeTrue();

            status4.Value.Should().NotBeSameAs(cat);
        }
    }


}
