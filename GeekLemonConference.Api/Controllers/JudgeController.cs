using GeekLemonConference.Application.CQRS.Queries.Judges.GetAllJudges;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekLemonConference.Application.CQRS.Judges.Commands.CreateJudge;
using GeekLemonConference.Application.CQRS.Judges.Commands.UpdateJudge;
using GeekLemonConference.Application.CQRS.Judges.Commands.DeleteJudge;
using GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Api.Models;
using GeekLemonConference.Application.CQRS.Dto;

namespace GeekLemonConference.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgeController : BaseGeekLemonController
    {
        private readonly IMediator _mediator;

        public JudgeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "getalljudges")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<JudgesInListViewModel>>> GetAllPosts()
        {
            var result = await _mediator.Send(new GetJudgesInListQuery());

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

        [HttpGet("byid/{id}", Name = "getjudge")]
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
                (new GetJudgeQuery() { JudeId = new JudgeId(id) });

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

        [HttpGet("byuqniqueid/{uid}", Name = "GetJudgeByUniqueId")]
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
                (new GetJudgeQuery() { JudgeUniqueId = new JudgeUniqueId(uid) });

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

        [HttpPost(Name = "addjudge")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JudgeIds>> Create([FromBody] CreateJudgeRequest judge)
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

        [HttpPut("ByUniqueId", Name = "UpdateByUniqueId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateByUniqueId([FromBody] UpdateJudgeByUniqueId u)
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

        [HttpPut("ById", Name = "UpdateById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateById([FromBody] UpdateJudgeById u)
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

        [HttpDelete("byuniqueId/{id}", Name = "deletejudgedByUniqueId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteByUniqueId(Guid id)
        {
            var deletepostCommand = new DeleteJudgeCommand()
            {
                UniqueId
                = new JudgeUniqueId(id)
            };

            var result = await _mediator.Send(deletepostCommand);

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

        [HttpDelete("byId/{id}", Name = "deletejudgebyid")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteById(int id)
        {
            var deletepostCommand = new DeleteJudgeCommand()
            {
                Id
                = new JudgeId(id)
            };

            var result = await _mediator.Send(deletepostCommand);
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
