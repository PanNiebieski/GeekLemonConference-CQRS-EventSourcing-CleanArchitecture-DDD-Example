using GeekLemonConference.Api.Models;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.AcceptCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.PreliminaryAcceptCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.RejectCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.SubmitCallForSpeech;
using GeekLemonConference.Application.CQRS.Categories.Commands.UpdateCategory;
using GeekLemonConference.Application.CQRS.Categories.Queries.GetCategory;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Judges.Commands.CreateJudge;
using GeekLemonConference.Application.CQRS.Judges.Commands.DeleteJudge;
using GeekLemonConference.Application.CQRS.Judges.Commands.UpdateJudge;
using GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge;
using GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetAllCallForSpeeches;
using GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetCallForSpeech;
using GeekLemonConference.Application.CQRS.Queries.Categories.GetAllCategories;
using GeekLemonConference.Application.CQRS.Queries.Judges.GetAllJudges;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Infrastructure.EventStore.SQLite;
using GeekLemonConference.Infrastructure.EventStoreAndBus;
using GeekLemonConference.Persistence.Dapper.SQLite;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGeekLemonConferenceCQRS(configuration);
builder.Services.AddGeekLemonPersistenceDapperSQLiteServices(configuration);
//services.AddDefaultEventStore();
builder.Services.AddEventStoreSqlLite(configuration);
//services.AddEventStoreMongoDb(Configuration);
builder.Services.AddBusAndRepository(configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open",
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors("Open");

IMediator _mediator = app.Services.GetService<IMediator>();


app.MapGet("/api/callforspeech/all/{filter}", async (int filter) =>
{
    GetAllCallForSpeechesQuery getAllCallForSpeechesQuery = new GetAllCallForSpeechesQuery()
    {
        Filter = (FilterCallForSpeechStyles)filter,
        queryWitchDataBase = QueryWitchDataBase.NormalCQRS
    };

    var result = await _mediator.Send(getAllCallForSpeechesQuery);

    if (result.Status == ResponseStatus.BussinesLogicError)
        return Results.Forbid();
    if (result.Status == ResponseStatus.NotFoundInDataBase)
        return Results.NotFound();
    if (result.Status == ResponseStatus.ValidationError)
        return Results.BadRequest();
    if (result.Status == ResponseStatus.BadQuery)
        return Results.BadRequest();
    //if (!result.Success)
    //    return MethodFailure(result.Message);

    return Results.Ok(result.List);

})
.WithName("getallcallforspeeches");

app.MapGet("/api/callforspeech/id/{id}", async (int id) =>
{
    var result = await _mediator.Send(
        (new GetCallForSpeechQuery()
        {
            CallForSpeechId = new CallForSpeechId(id),
            queryWitchDataBase = QueryWitchDataBase.NormalCQRS
        }));

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.Ok(result.CallForSpeech);

})
.WithName("GetCallForSpeechById");


app.MapGet("/api/callforspeech/uniqueid/{uid}", async (Guid uid) =>
{
    var result = await _mediator.Send(
        (new GetCallForSpeechQuery()
        {
            CallForSpeechUniqueId = new CallForSpeechUniqueId(uid),
            queryWitchDataBase = QueryWitchDataBase.NormalCQRS
        }));

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.Ok(result.CallForSpeech);

})
.WithName("GetCallForSpeechByUniqueId");

app.MapPost("/api/callforspeech/submit", async (CreateCallForSpeaker request) =>
{
    SubmitCallForSpeechCommand s = new SubmitCallForSpeechCommand()
    {
        Speaker = request.Speaker,
        CategoryId = request.CategoryId,
        Speech = request.Speech,
        Number = request.Number,
    };

    var result = await _mediator.Send(s);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.Ok(result.CallForSpeechCommandIds);

})
.WithName("Ssubmit");


app.MapPost("/api/callforspeech/rejectcallforspeech", async (RejectCallForSpeechCommand rejectCallForSpeechCommand) =>
{
    var result = await _mediator.Send(rejectCallForSpeechCommand);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.NoContent();

})
.WithName("Rejectcallforspeech");

app.MapPost("/api/callforspeech/accept", async (AcceptCallForSpeechCommand ccceptCallForSpeechCommand) =>
{
    var result = await _mediator.Send(ccceptCallForSpeechCommand);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);

    if (action != null)
        return action;

    return Results.NoContent();
})
.WithName("accept");

app.MapPost("/api/callforspeech/preliminaryacceptcallforspeech", async (PreliminaryAcceptCallForSpeechCommand preliminaryCallForSpeechCommand) =>
{
    var result = await _mediator.Send(preliminaryCallForSpeechCommand);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action == null)
        return Results.NoContent();
    else
        return action;
})
.WithName("preliminaryacceptcallforspeech");


app.MapGet("/api/category/all", async () =>
{
    var result = await _mediator.Send(new GetCategoriesListQuery());

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);

    if (action != null)
        return action;

    return Results.Ok(result.List);

})
.WithName("getallcategories");

app.MapGet("/api/category/byId/{id}", async (int id) =>
{
    var result = await _mediator.Send(
        (new GetCallForSpeechQuery()
        {
            CallForSpeechId = new CallForSpeechId(id),
            queryWitchDataBase = QueryWitchDataBase.NormalCQRS
        }));

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);

    if (action != null)
        return action;

    return Results.Ok(result.CallForSpeech);

})
.WithName("GetCategoryById");

app.MapGet("/api/category/byuniqueid/{uid}", async (Guid uid) =>
{
    var result = await _mediator.Send
        (new GetCategoryQuery()
        { CategoryUniqueId = new CategoryUniqueId(uid) });

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);

    if (action != null)
        return action;

    return Results.Ok(result.Category);
})
.WithName("GetCategoryByUniqueId");


app.MapPut("/api/category/UpdateById", async (UpdateCategoryById u) =>
{
    UpdateCategoryCommand updateCommand = new UpdateCategoryCommand()
    {
        DisplayName = u.DisplayName,
        Id = u.Id,
        Name = u.Name,
        WhatWeAreLookingFor = u.WhatWeAreLookingFor
    };

    var result = await _mediator.Send(updateCommand);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.NoContent();
})
.WithName("updateByIdcategory");

app.MapPut("/api/category/UpdateByUniqueId", async (UpdateCategoryByUniqueId u) =>
{
    UpdateCategoryCommand updateCommand = new UpdateCategoryCommand()
    {
        DisplayName = u.DisplayName,
        UniqueId = u.UniqueId,
        Name = u.Name,
        WhatWeAreLookingFor = u.WhatWeAreLookingFor
    };

    var result = await _mediator.Send(updateCommand);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.NoContent();
})
.WithName("UpdateByUniqueId");


app.MapGet("/api/judge/all", async () =>
{
    var result = await _mediator.Send(new GetJudgesInListQuery());

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.Ok(result.List);
})
.WithName("getalljudges");


app.MapGet("/api/judge/byid/{id}", async (int id) =>
{
    var result = await _mediator.Send
        (new GetJudgeQuery() { JudeId = new JudgeId(id) });

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.Ok(result.Judge);
})
.WithName("getjudge");


app.MapGet("/api/judge/byuqniqueid/{uid}", async (Guid uid) =>
{
    var result = await _mediator.Send
    (new GetJudgeQuery() { JudgeUniqueId = new JudgeUniqueId(uid) });

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.Ok(result.Judge);
})
.WithName("GetJudgeByUniqueId");


app.MapPost("/api/judge/addjudge", async ([FromBody] CreateJudgeRequest judge) =>
{
    CreateJudgeCommand c = new CreateJudgeCommand()
    {
        Birthdate = judge.Birthdate,
        CategoryId = judge.CategoryId,
        Login = judge.Login,
        Name = judge.Name,
        Password = judge.Password,
    };

    var result = await _mediator.Send(c);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.Ok(result.JudgeIds);
})
.WithName("addjudge");


app.MapPut("/api/judge/UpdateByUniqueId", async ([FromBody] UpdateJudgeByUniqueId u) =>
{
    UpdateJudgeCommand updateJudge = new UpdateJudgeCommand()
    {
        UniqueId = u.UniqueId,
        Name = u.Name,
        Login = u.Password,
        Password = u.Password,
        Birthdate = u.Birthdate,
        CategoryId = u.CategoryId,
    };

    var result = await _mediator.Send(updateJudge);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.NoContent();
})
.WithName("JudgeUpdateByUniqueId");

app.MapPut("/api/judge/UpdateById", async ([FromBody] UpdateJudgeById u) =>
{
    UpdateJudgeCommand updateJudge = new UpdateJudgeCommand()
    {
        Id = u.Id,
        Name = u.Name,
        Login = u.Password,
        Password = u.Password,
        Birthdate = u.Birthdate,
        CategoryId = u.CategoryId,
    };

    var result = await _mediator.Send(updateJudge);


    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.NoContent();
})
.WithName("JudgeUpdateById");

app.MapDelete("/api/judge/DeleteByUniqueId/{id}", async (Guid id) =>
{
    var deletepostCommand = new DeleteJudgeCommand()
    {
        UniqueId = new JudgeUniqueId(id)
    };

    var result = await _mediator.Send(deletepostCommand);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.NoContent();
})
.WithName("deletejudgedByUniqueId");

app.MapDelete("/api/judge/DeleteById/{id}", async (int id) =>
{
    var deletepostCommand = new DeleteJudgeCommand()
    {
        Id
        = new JudgeId(id)
    };

    var result = await _mediator.Send(deletepostCommand);

    var action = ChooseFailStatusBaseOnResult(result.Status, result.Success, result.Message);
    if (action != null)
        return action;

    return Results.NoContent();
})
.WithName("deletejudgebyid");


app.Run();



IResult ChooseFailStatusBaseOnResult(ResponseStatus status, bool IsSuccess, string message)
{
    if (status == ResponseStatus.BussinesLogicError)
        return Results.Forbid();
    if (status == ResponseStatus.NotFoundInDataBase)
        return Results.NotFound();
    if (status == ResponseStatus.ValidationError)
        return Results.BadRequest();
    if (status == ResponseStatus.BadQuery)
        return Results.BadRequest();
    if (!IsSuccess)
        return new MethodFailure(message);
    return null;
}