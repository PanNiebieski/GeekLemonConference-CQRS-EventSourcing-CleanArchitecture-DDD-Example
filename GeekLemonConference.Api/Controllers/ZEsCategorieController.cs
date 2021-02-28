using GeekLemonConference.Application.CQRS.Categories.Commands.UpdateCategory;
using GeekLemonConference.Application.CQRS.Categories.Queries.GetCategory;
using GeekLemonConference.Application.CQRS.Commands.Categories.CreateCategory;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Queries.Categories.GetAllCategories;
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
using GeekLemonConference.Application.CQRS.Categories.CommandsEs.CreateCategory;
using GeekLemonConference.Application.CQRS.Categories.CommandsEs.UpdateCategory;

namespace GeekLemonConference.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZEsCategorieController : BaseGeekLemonController
    {
        private readonly IMediator _mediator;

        public ZEsCategorieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "getallcategoriesES")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryInListViewModel>>> GetAllCategories()
        {
            var result = await _mediator.Send(
                new GetCategoriesListQuery()
                {
                    queryWitchDataBase =
                        QueryWitchDataBase.WithEventSourcing
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

        [HttpGet("{id}", Name = "GetCategoryByIdEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var result = await _mediator.Send
                (new GetCategoryQuery()
                {
                    CategoryId = new CategoryId(id),
                    queryWitchDataBase = QueryWitchDataBase
                    .WithEventSourcing
                });

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return Forbid(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return Ok(result.Category);
        }

        [HttpGet("{uid}", Name = "GetCategoryByUniqueIdEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CategoryDto>> GetCategoryByUniqueId(Guid uid)
        {
            var result = await _mediator.Send
                (new GetCategoryQuery()
                {
                    CategoryUniqueId = new CategoryUniqueId(uid),
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

            return Ok(result.Category);
        }

        [HttpPut("ByUniqueId", Name = "updatecategoryEs")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] EsUpdateCategoryCommand updatePostCommand)
        {
            var result = await _mediator.Send(updatePostCommand);

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

        [HttpPost(Name = "addcategoryEs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryIds>> Create
            ([FromBody] ESCreateCategoryCommand createCategoryCommand)
        {
            var result = await _mediator.Send(createCategoryCommand);

            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.Forbid)
                return Forbid(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.NotFound)
                return NotFound(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.BadRequest)
                return BadRequest(result.Message);
            if (result.WhatHTTPCodeToBeRetruned == WhatHTTPCodeShouldBeRetruned.MethodFailure)
                return MethodFailure(result.Message);

            return Ok(result.CategoryIds);
        }
    }
}
