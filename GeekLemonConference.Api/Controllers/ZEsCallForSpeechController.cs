using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.AcceptCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.EvaluateCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.PreliminaryAcceptCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.RejectCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.SubmitCallForSpeech;
using GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetAllCallForSpeeches;
using GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetCallForSpeech;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekLemonConference.Application.CQRS;
using GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.SubmitCallForSpeech;
using GeekLemonConference.Api.Models;
using GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.EvaluateCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.AcceptCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.PreliminaryAcceptCallForSpeech;

namespace GeekLemonConference.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZEsCallForSpeechController : BaseGeekLemonController
    {
        private readonly IMediator _mediator;

        public ZEsCallForSpeechController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all/{filter}", Name = "getallcallforspeechesEs")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CallForSpeechInListViewModel>>> GetAllCallForSpeeches
            (int filter)
        {
            GetAllCallForSpeechesQuery getAllCallForSpeechesQuery = new GetAllCallForSpeechesQuery()
            {
                Filter = (FilterCallForSpeechStyles)filter,
                queryWitchDataBase = QueryWitchDataBase.WithEventSourcing
            };

            var result = await _mediator.Send(getAllCallForSpeechesQuery);

            if (result.Status == ResponseStatus.BussinesLogicError)
                return Forbid();
            if (result.Status == ResponseStatus.NotFoundInDataBase)
                return NotFound();
            if (result.Status == ResponseStatus.ValidationError)
                return BadRequest();
            if (result.Status == ResponseStatus.BadQuery)
                return BadRequest();
            if (!result.Success)
                return MethodFailure(result.Message);


            return Ok(result.List);
        }

        [HttpGet("{id}", Name = "GetCallForSpeechByIdEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CallForSpeechViewModel>> GetCallForSpeechById(int id)
        {
            var result = await _mediator.Send(
                (new GetCallForSpeechQuery()
                {
                    CallForSpeechId = new CallForSpeechId(id),
                    queryWitchDataBase = QueryWitchDataBase.WithEventSourcing
                }));

            if (result.Status == ResponseStatus.BussinesLogicError)
                return Forbid();
            if (result.Status == ResponseStatus.NotFoundInDataBase)
                return NotFound();
            if (result.Status == ResponseStatus.ValidationError)
                return BadRequest();
            if (result.Status == ResponseStatus.BadQuery)
                return BadRequest();
            if (!result.Success)
                return MethodFailure(result.Message);

            return Ok(result.CallForSpeech);
        }

        [HttpGet("{uid}", Name = "GetCallForSpeechByUniqueIdEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CallForSpeechViewModel>> GetCallForSpeechByUniqueId(Guid uid)
        {
            var result = await _mediator.Send(
                (new GetCallForSpeechQuery()
                {
                    CallForSpeechUniqueId = new CallForSpeechUniqueId(uid),
                    queryWitchDataBase = QueryWitchDataBase.WithEventSourcing
                }));

            return Ok(result.CallForSpeech);
        }


        [HttpPost("submit", Name = "submitcallforspeechEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> Submit([FromBody] CreateCallForSpeaker r)
        {
            EsSubmitCallForSpeechCommand c = new EsSubmitCallForSpeechCommand()
            {
                CategoryId = r.CategoryId,
                Speaker = r.Speaker,
                Speech = r.Speech,
                Number = r.Number,
            };

            var result = await _mediator.Send(c);

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return Forbid(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return Ok(result);
        }

        [HttpPost("reject", Name = "rejectcallforspeechEs")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> Reject([FromBody] RejectCallForSpeechCommand rejectCallForSpeechCommand)
        {
            var result = await _mediator.Send(rejectCallForSpeechCommand);

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return Forbid(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return NoContent();
        }

        [HttpPost("evaluate", Name = "evaluatecallforspeechEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Evaluate([FromBody] EsEvaluateCallForSpeechCommand evaluateCallForSpeechCommand)
        {
            var result = await _mediator.Send(evaluateCallForSpeechCommand);

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return Forbid(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return Ok(result);
        }

        [HttpPost("accept", Name = "acceptcallforspeechEs")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Accept([FromBody] EsAcceptCallForSpeechComand ccceptCallForSpeechCommand)
        {
            var result = await _mediator.Send(ccceptCallForSpeechCommand);

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return NoContent();
        }

        [HttpPost("preliminaryaccept", Name = "preliminaryacceptcallforspeechEs")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> PreliminaryAccept([FromBody] EsPreliminaryAcceptCallForSpeechCommand preliminaryCallForSpeechCommand)
        {
            var result = await _mediator.Send(preliminaryCallForSpeechCommand);

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return Forbid(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return NoContent();
        }
    }
}
