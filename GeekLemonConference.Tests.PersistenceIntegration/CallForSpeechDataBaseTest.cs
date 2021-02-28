using AutoMapper;
using FluentAssertions;
using GeekLemon.Infrastructure.Read.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.Map;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Status;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.CallForSpeeches;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges;
using GeekLemonConference.Persistence.Dapper.SQLite.Repositories.CallForSpeeches;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static GeekLemonConference.Tests.Builders.CallForSpeechBuilder;
using static GeekLemonConference.Tests.Builders.CategoryBuilder;
using static GeekLemonConference.Tests.Builders.JudgeBuilder;

namespace GeekLemonConference.IntegrationTests.Persistence
{
    public class CallForSpeechDataBaseTest
    {
        private GeekLemonDBContext _geekLemonContex;
        private IMapper _mapper;
        private string _tempdatabasefile;

        public CallForSpeechDataBaseTest()
        {
            _tempdatabasefile = CopyDataBase.Run();

            ExecutionFlow.Options = new
                ExecutionOptions(true);


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



        public async Task CreateCategoriesAsync()
        {
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat1 = GivenCategory().WithId(1).WithName("AAA")
                .Build();


            var cat2 = GivenCategory().WithId(2).WithName("GGG")
                .Build();

            var st1 = await categoryAddDoer.Run(cat1);
            var st2 = await categoryAddDoer.Run(cat2);
        }

        public async Task CreateJudgeAsync()
        {
            JudgeAddDoer judgeAddDoer =
                new JudgeAddDoer(_geekLemonContex, _mapper);

            var judge1 = GivenJudge().WithCategory(c => c.WithId(1)).Build();
            var judge2 = GivenJudge().WithCategory(c => c.WithId(2)).Build();

            var status1 = await judgeAddDoer.Run(judge1);
            var status2 = await judgeAddDoer.Run(judge2);
        }

        [Fact]
        public async Task SubmitCallForSpeechShouldBeSuccess()
        {
            CategoryAddDoer categoryAddDoer =
                new CategoryAddDoer(_geekLemonContex, _mapper);

            var cat = GivenCategory().WithId(1).WithName("AAA")
                .Build();

            var st1 = await categoryAddDoer.Run(cat);

            //SetupCategories();
            CallForSpeechSubmitDoer callForSpeechSubmitDoer =
                new CallForSpeechSubmitDoer(_geekLemonContex, _mapper);

            var cfs = GivenCallForSpeech().WithCategory
                (c => c.WithId(1))
                .Build();

            var status = await callForSpeechSubmitDoer.Run(cfs);

            status.Success.Should().BeTrue();
        }

        [Fact]
        public async Task RejectCallForSpeechByIdShouldBeSuccess()
        {
            CreateCategoriesAsync();
            CreateJudgeAsync();

            CallForSpeechSubmitDoer callForSpeechSubmitDoer =
                new CallForSpeechSubmitDoer(_geekLemonContex, _mapper);

            var cfs = GivenCallForSpeech().WithCategory
                (c => c.WithId(1))
                .Build();

            var status = await callForSpeechSubmitDoer.Run(cfs);
            status.Success.Should().BeTrue();

            CallForSpeechSaveRejectionDoer callForSpeechSaveRejectionDoer =
                new CallForSpeechSaveRejectionDoer(_geekLemonContex, _mapper);

            JudgeId judgeId = new JudgeId(1);

            var status2 = await callForSpeechSaveRejectionDoer.Run(cfs.Id, judgeId,
                Domain.ValueObjects.CallForSpeechStatus.Rejected);

            status2.Success.Should().BeTrue();

            CallForSpeechGetByIdDoer callForSpeechGetByIdDoer =
                new CallForSpeechGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await callForSpeechGetByIdDoer.Run
                (status.Value.CreatedId as CallForSpeechId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(cfs);
        }


        [Fact]
        public async Task RejectCallForSpeechByUniqueIdShouldBeSuccess()
        {
            CreateCategoriesAsync();
            CreateJudgeAsync();

            CallForSpeechSubmitDoer callForSpeechSubmitDoer =
    new CallForSpeechSubmitDoer(_geekLemonContex, _mapper);

            var cfs = GivenCallForSpeech().WithCategory
                (c => c.WithId(1))
                .Build();

            var status = await callForSpeechSubmitDoer.Run(cfs);

            CallForSpeechSaveRejectionDoer callForSpeechSaveRejectionDoer =
                new CallForSpeechSaveRejectionDoer(_geekLemonContex, _mapper);


            JudgeId judgeId = new JudgeId(1);

            var status2 = await callForSpeechSaveRejectionDoer.Run(cfs.UniqueId, judgeId,
                Domain.ValueObjects.CallForSpeechStatus.Rejected);

            status2.Success.Should().BeTrue();

            CallForSpeechGetByIdDoer callForSpeechGetByIdDoer =
    new CallForSpeechGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await callForSpeechGetByIdDoer.Run
                (status.Value.UniqueId as CallForSpeechUniqueId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(cfs);
        }

        [Fact]
        public async Task AcceptCallForSpeechByIdShouldBeSuccess()
        {
            CreateCategoriesAsync();
            CreateJudgeAsync();

            CallForSpeechSubmitDoer callForSpeechSubmitDoer =
    new CallForSpeechSubmitDoer(_geekLemonContex, _mapper);

            var cfs = GivenCallForSpeech().WithCategory
                (c => c.WithId(1))
                .Build();

            var status = await callForSpeechSubmitDoer.Run(cfs);

            CallForSpeechSaveAcceptenceDoer callForSpeechSaveAcceptenceDoer =
                new CallForSpeechSaveAcceptenceDoer(_geekLemonContex, _mapper);

            JudgeId judgeId = new JudgeId(1);

            var status2 = await callForSpeechSaveAcceptenceDoer.Run(cfs.Id, judgeId,
                Domain.ValueObjects.CallForSpeechStatus.AcceptedByJudge);

            status2.Success.Should().BeTrue();

            CallForSpeechGetByIdDoer callForSpeechGetByIdDoer =
    new CallForSpeechGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await callForSpeechGetByIdDoer.Run
                (status.Value.CreatedId as CallForSpeechId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(cfs);
        }

        [Fact]
        public async Task PreliminaryAcceptenceCallForSpeechByIdShouldBeSuccess()
        {
            CreateCategoriesAsync();
            CreateJudgeAsync();

            CallForSpeechSubmitDoer callForSpeechSubmitDoer =
    new CallForSpeechSubmitDoer(_geekLemonContex, _mapper);

            var cfs = GivenCallForSpeech().WithCategory
                (c => c.WithId(1))
                .Build();

            var status = await callForSpeechSubmitDoer.Run(cfs);

            CallForSpeechSavePreliminaryAcceptenceDoer callForSpeechSavePreliminaryAcceptenceDoer =
                new CallForSpeechSavePreliminaryAcceptenceDoer(_geekLemonContex, _mapper);

            JudgeId judgeId = new JudgeId(1);

            var status2 = await callForSpeechSavePreliminaryAcceptenceDoer.Run(cfs.Id, judgeId,
                Domain.ValueObjects.CallForSpeechStatus.AcceptedByJudge);

            status2.Success.Should().BeTrue();

            CallForSpeechGetByIdDoer callForSpeechGetByIdDoer =
    new CallForSpeechGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await callForSpeechGetByIdDoer.Run
                (status.Value.CreatedId as CallForSpeechId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(cfs);
        }

        [Fact]
        public async Task AcceptCallForSpeechByUniqueIdShouldBeSuccess()
        {
            CreateCategoriesAsync();
            CreateJudgeAsync();

            CallForSpeechSubmitDoer callForSpeechSubmitDoer =
    new CallForSpeechSubmitDoer(_geekLemonContex, _mapper);

            var cfs = GivenCallForSpeech().WithCategory
                (c => c.WithId(1))
                .Build();

            var status = await callForSpeechSubmitDoer.Run(cfs);

            CallForSpeechSaveAcceptenceDoer callForSpeechSaveAcceptenceDoer =
                new CallForSpeechSaveAcceptenceDoer(_geekLemonContex, _mapper);

            JudgeId judgeId = new JudgeId(1);

            var status2 = await callForSpeechSaveAcceptenceDoer.Run(cfs.UniqueId, judgeId,
                Domain.ValueObjects.CallForSpeechStatus.AcceptedByJudge);

            status2.Success.Should().BeTrue();

            CallForSpeechGetByIdDoer callForSpeechGetByIdDoer =
                new CallForSpeechGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await callForSpeechGetByIdDoer.Run
                (status.Value.UniqueId as CallForSpeechUniqueId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(cfs);
        }

        [Fact]
        public async Task PreliminaryAcceptenceCallForSpeechByUniqueIdShouldBeSuccess()
        {
            CreateCategoriesAsync();
            CreateJudgeAsync();

            CallForSpeechSubmitDoer callForSpeechSubmitDoer =
    new CallForSpeechSubmitDoer(_geekLemonContex, _mapper);

            var cfs = GivenCallForSpeech().WithCategory
                (c => c.WithId(1))
                .Build();

            var status = await callForSpeechSubmitDoer.Run(cfs);

            CallForSpeechSavePreliminaryAcceptenceDoer callForSpeechSavePreliminaryAcceptenceDoer =
                new CallForSpeechSavePreliminaryAcceptenceDoer(_geekLemonContex, _mapper);

            JudgeId judgeId = new JudgeId(1);

            var status2 = await callForSpeechSavePreliminaryAcceptenceDoer.Run(cfs.UniqueId, judgeId,
                Domain.ValueObjects.CallForSpeechStatus.AcceptedByJudge);

            status2.Success.Should().BeTrue();

            CallForSpeechGetByIdDoer callForSpeechGetByIdDoer =
                new CallForSpeechGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await callForSpeechGetByIdDoer.Run
                (status.Value.UniqueId as CallForSpeechUniqueId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(cfs);
        }

        [Fact]
        public async Task EvaluatedCallForSpeechByUniqueIdShouldBeSuccess()
        {
            CreateCategoriesAsync();
            CreateJudgeAsync();

            CallForSpeechSubmitDoer callForSpeechSubmitDoer =
    new CallForSpeechSubmitDoer(_geekLemonContex, _mapper);

            var cfs = GivenCallForSpeech().WithCategory
                (c => c.WithId(1))
                .Evaluated().Build();

            var status = await callForSpeechSubmitDoer.Run(cfs);

            CallForSpeechSaveEvaluatationDoer callForSpeechSaveEvaluatationDoer =
                new CallForSpeechSaveEvaluatationDoer(_geekLemonContex, _mapper);

            JudgeId judgeId = new JudgeId(1);

            var status2 = await callForSpeechSaveEvaluatationDoer.Run
                (cfs.UniqueId, cfs.Score, cfs.Status);

            status2.Success.Should().BeTrue();

            CallForSpeechGetByIdDoer callForSpeechGetByIdDoer =
                new CallForSpeechGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await callForSpeechGetByIdDoer.Run
                (status.Value.UniqueId as CallForSpeechUniqueId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(cfs);
        }

        [Fact]
        public async Task EvaluatedCallForSpeechByIdShouldBeSuccess()
        {
            CreateCategoriesAsync();
            CreateJudgeAsync();

            CallForSpeechSubmitDoer callForSpeechSubmitDoer =
    new CallForSpeechSubmitDoer(_geekLemonContex, _mapper);

            var cfs = GivenCallForSpeech().WithCategory
                (c => c.WithId(1))
                .Evaluated().Build();

            var status = await callForSpeechSubmitDoer.Run(cfs);

            CallForSpeechSaveEvaluatationDoer callForSpeechSaveEvaluatationDoer =
                new CallForSpeechSaveEvaluatationDoer(_geekLemonContex, _mapper);

            JudgeId judgeId = new JudgeId(1);

            var status2 = await callForSpeechSaveEvaluatationDoer.Run
                (cfs.Id, cfs.Score, cfs.Status);

            status2.Success.Should().BeTrue();

            CallForSpeechGetByIdDoer callForSpeechGetByIdDoer =
                new CallForSpeechGetByIdDoer(_geekLemonContex, _mapper);

            var status3 = await callForSpeechGetByIdDoer.Run
                (status.Value.CreatedId as CallForSpeechId);

            status3.Success.Should().BeTrue();

            status3.Value.Should().NotBeSameAs(cfs);
        }
    }
}
