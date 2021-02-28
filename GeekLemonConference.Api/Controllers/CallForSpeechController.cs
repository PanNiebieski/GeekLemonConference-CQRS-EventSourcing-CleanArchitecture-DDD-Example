using GeekLemonConference.Application.Common;
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
using GeekLemonConference.Api.Models;

namespace GeekLemonConference.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallForSpeechController : BaseGeekLemonController
    {
        private readonly IMediator _mediator;

        public CallForSpeechController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all/{filter}", Name = "getallcallforspeeches")]
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
                queryWitchDataBase = QueryWitchDataBase.NormalCQRS
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

        [HttpGet("id/{id}", Name = "GetCallForSpeechById")]
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
                    queryWitchDataBase = QueryWitchDataBase.NormalCQRS
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

        [HttpGet("uniqueid/{uid}", Name = "GetCallForSpeechByUniqueId")]
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
                    queryWitchDataBase = QueryWitchDataBase.NormalCQRS
                }));

            return Ok(result.CallForSpeech);
        }

        [HttpPost("submit", Name = "submitcallforspeech")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CallForSpeechIds>> Submit([FromBody] CreateCallForSpeaker request)
        {
            SubmitCallForSpeechCommand s = new SubmitCallForSpeechCommand()
            {
                Speaker = request.Speaker,
                CategoryId = request.CategoryId,
                Speech = request.Speech,
                Number = request.Number,
            };

            var result = await _mediator.Send(s);

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

            return Ok(result.CallForSpeechCommandIds);
        }

        [HttpPost("reject", Name = "rejectcallforspeech")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Reject([FromBody] RejectCallForSpeechCommand rejectCallForSpeechCommand)
        {
            var result = await _mediator.Send(rejectCallForSpeechCommand);

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

            return NoContent();
        }

        [HttpPost("evaluate", Name = "evaluatecallforspeech")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Evaluate([FromBody] EvaluateCallForSpeechCommand evaluateCallForSpeechCommand)
        {
            var result = await _mediator.Send(evaluateCallForSpeechCommand);

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

            return Ok(result);
        }

        [HttpPost("accept", Name = "acceptcallforspeech")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Accept([FromBody] AcceptCallForSpeechCommand ccceptCallForSpeechCommand)
        {
            var result = await _mediator.Send(ccceptCallForSpeechCommand);

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

            return NoContent();
        }

        [HttpPost("preliminaryaccept", Name = "preliminaryacceptcallforspeech")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> PreliminaryAccept([FromBody] PreliminaryAcceptCallForSpeechCommand preliminaryCallForSpeechCommand)
        {
            var result = await _mediator.Send(preliminaryCallForSpeechCommand);

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

            return NoContent();
        }




    }
}
