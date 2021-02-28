using AutoMapper;
using FluentAssertions;
using GeekLemon.Infrastructure.Read.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.Map;
using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static GeekLemonConference.Tests.Builders.CategoryBuilder;
using static GeekLemonConference.Tests.Builders.JudgeBuilder;

namespace GeekLemonConference.IntegrationTests.Persistence
{
    public class JudgeDataBaseTest
    {
        private GeekLemonDBContext _geekLemonContex;
        private IMapper _mapper;
        private string _tempdatabasefile;

        public JudgeDataBaseTest()
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
            try
            {
                File.Delete(_tempdatabasefile);
            }
            catch (Exception)
            { }
        }

        public async Task CreateCategoryAsync()
        {
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat = GivenCategory().WithId(1).WithName("AAA")
                .Build();

            var st1 = await categoryAddDoer.Run(cat);
        }

        [Fact]
        public async Task CreateJudgeShouldBeSuccessThenGetByIdThatJudge()
        {
            CreateCategoryAsync();

            JudgeAddDoer judgeAddDoer =
                new JudgeAddDoer(_geekLemonContex, _mapper);

            var judge = GivenJudge().WithCategory(c => c.WithId(1)).Build();

            var status = await judgeAddDoer.Run(judge);

            status.Success.Should().BeTrue();

            JudgeGetByIdDoer judgeGetByIdDoer =
                new JudgeGetByIdDoer(_geekLemonContex, _mapper);

            var status2 = await judgeGetByIdDoer.Run(
                status.Value.CreatedId as JudgeId);

            status2.Success.Should().BeTrue();

            status2.Value.Should().Equals(judge);
        }



        [Fact]
        public async Task CreateJudgeShouldBeSuccessThenGetByUniqueIdThatJudge()
        {
            CreateCategoryAsync();

            JudgeAddDoer judgeAddDoer =
                new JudgeAddDoer(_geekLemonContex, _mapper);

            var judge = GivenJudge().WithCategory(c => c.WithId(1)).Build();

            var status = await judgeAddDoer.Run(judge);

            status.Success.Should().BeTrue();

            JudgeGetByIdDoer judgeGetByIdDoer =
                new JudgeGetByIdDoer(_geekLemonContex, _mapper);

            var status2 = await judgeGetByIdDoer.Run(
                status.Value.UniqueId as JudgeUniqueId);

            status2.Success.Should().BeTrue();

            status2.Value.Should().Equals(judge);
        }

        [Fact]
        public async Task CreateJudgeShouldBeSuccessThenDeleteThenTryGetByIdAndFail()
        {
            CreateCategoryAsync();

            JudgeAddDoer judgeAddDoer =
                new JudgeAddDoer(_geekLemonContex, _mapper);

            var judge = GivenJudge().WithCategory(c => c.WithId(1)).Build();

            var status = await judgeAddDoer.Run(judge);

            status.Success.Should().BeTrue();

            JudgeDeleteDoer judgeDeleteDoer =
                new JudgeDeleteDoer(_geekLemonContex, _mapper);

            var status2 = await judgeDeleteDoer.Run(
                status.Value.CreatedId as JudgeId);

            status2.Success.Should().BeTrue();

            JudgeGetByIdDoer judgeGetByIdDoer =
                new JudgeGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await judgeGetByIdDoer.Run(
                status.Value.CreatedId as JudgeId);

            status3.Success.Should().BeFalse();
        }

        [Fact]
        public async Task CreateJudgeShouldBeSuccessThenDeleteByUniqueIdThenTryGetByUniqueIdAndFail()
        {
            CreateCategoryAsync();

            JudgeAddDoer judgeAddDoer =
                new JudgeAddDoer(_geekLemonContex, _mapper);

            var judge = GivenJudge().WithCategory(c => c.WithId(1)).Build();

            var status = await judgeAddDoer.Run(judge);

            status.Success.Should().BeTrue();

            JudgeDeleteDoer judgeDeleteDoer =
                new JudgeDeleteDoer(_geekLemonContex, _mapper);

            var status2 = await judgeDeleteDoer.Run(
                status.Value.UniqueId as JudgeUniqueId);

            status2.Success.Should().BeTrue();

            JudgeGetByIdDoer judgeGetByIdDoer =
                new JudgeGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await judgeGetByIdDoer.Run(
                status.Value.UniqueId as JudgeUniqueId);

            status3.Success.Should().BeFalse();
        }

        [Fact]
        public async Task CreateJudgeShouldBeSuccessThenGetByUniqueIdShouldbeAlsoASuccess()
        {
            CreateCategoryAsync();

            JudgeAddDoer judgeAddDoer =
                new JudgeAddDoer(_geekLemonContex, _mapper);

            var judge = GivenJudge().WithCategory(c => c.WithId(1)).Build();

            var status = await judgeAddDoer.Run(judge);

            status.Success.Should().BeTrue();

            JudgeGetAllDoer judgeGetAllDoer =
                new JudgeGetAllDoer(_geekLemonContex, _mapper);

            var status2 = await judgeGetAllDoer.Run();

            status2.Success.Should().BeTrue();

            status2.Value.Should().HaveCount(1);
        }

        [Fact]
        public async Task CreateJudgeThenUpdatedShoudlBeSuccessById()
        {
            CreateCategoryAsync();
            JudgeAddDoer judgeAddDoer =
                new JudgeAddDoer(_geekLemonContex, _mapper);

            var judge = GivenJudge().WithCategory(c => c.WithId(1))
                .WithId(1).Build();

            var status = await judgeAddDoer.Run(judge);

            status.Success.Should().BeTrue();

            JudgeUpdateDoer judgeUpdateDoer =
                new JudgeUpdateDoer(_geekLemonContex, _mapper);

            var judge2 = GivenJudge().WithId(1)
                .WithLogin("qwertyuiop")
                .Build();

            var status2 = await judgeUpdateDoer.Run(judge2, ByWhatId.CreatedId);
            status2.Success.Should().BeTrue();

            JudgeGetByIdDoer categoryGetByIdDoer =
                new JudgeGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await categoryGetByIdDoer.Run(
                status.Value.CreatedId as JudgeId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(judge);
        }
    }
}
