using GeekLemonConference.Application.CQRS.Judges.Commands.CreateJudge;
using GeekLemonConference.Application.CQRS.Judges.Commands.DeleteJudge;
using GeekLemonConference.Application.CQRS.Judges.Commands.UpdateJudge;
using GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge;
using GeekLemonConference.Application.CQRS.Queries.Judges.GetAllJudges;
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
using GeekLemonConference.Application.CQRS.Judges.CommandsEs.CreateJudge;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Api.Models;
using GeekLemonConference.Application.CQRS.Judges.CommandsEs.UpdateJudge;
using GeekLemonConference.Application.CQRS.Judges.CommandsEs.DeleteJudge;
using GeekLemonConference.Application.CQRS.Mapper.Dto;

namespace GeekLemonConference.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZEsJudgeController : BaseGeekLemonController
    {
        private readonly IMediator _mediator;

        public ZEsJudgeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "getalljudgesEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<JudgesInListViewModel>>> GetAllPosts()
        {
            var result = await _mediator.Send(
                new GetJudgesInListQuery()
                {
                    queryWitchDataBase = QueryWitchDataBase.WithEventSourcing
                }
                );

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return Forbid(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return Ok(result.List);
        }

        [HttpGet("{id}", Name = "getjudgeEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<JudgeViewModel>> GetJudgeById(int id)
        {
            var result = await _mediator.Send
                (new GetJudgeQuery()
                {
                    JudeId = new JudgeId(id),
                    queryWitchDataBase = QueryWitchDataBase.WithEventSourcing
                });

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return Forbid(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return Ok(result.Judge);
        }

        [HttpGet("{uid}", Name = "GetJudgeByUniqueIdEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<JudgeViewModel>> GetJudgeByUniqueId(Guid uid)
        {
            var result = await _mediator.Send
                (new GetJudgeQuery()
                {
                    JudgeUniqueId = new JudgeUniqueId(uid),
                    queryWitchDataBase = QueryWitchDataBase.WithEventSourcing
                }
                );

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return Forbid(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return Ok(result.Judge);
        }

        [HttpPost(Name = "addjudgeEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IdsDto>> Create([FromBody] CreateJudgeRequest judge)
        {
            EsCreateJudgeCommand c = new EsCreateJudgeCommand()
            {
                Birthdate = judge.Birthdate,
                CategoryId = judge.CategoryId,
                Login = judge.Login,
                Name = judge.Name,
                Password = judge.Password,
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

            return Ok(result.JudgeIds);
        }

        [HttpPut("ByUniqueId", Name = "updatejudgeEs")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateJudgeByUniqueId u)
        {
            EsUpdateJudgeCommand c = new EsUpdateJudgeCommand()
            {
                Birthdate = u.Birthdate,
                CategoryId = u.CategoryId,
                Login = u.Login,
                Name = u.Name,
                Version = u.Version,
                Password = u.Password,
                UniqueId = u.UniqueId
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

            return NoContent();
        }

        [HttpDelete("{uid}", Name = "deletejudgeEs")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid uid)
        {
            var deleteCommand = new EsDeleteJudgeCommand() { UniqueId = new JudgeUniqueId(uid) };
            var result = await _mediator.Send(deleteCommand);

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
