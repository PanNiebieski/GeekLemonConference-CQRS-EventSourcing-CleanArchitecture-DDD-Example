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
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Api.Models;

namespace GeekLemonConference.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseGeekLemonController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "getallcategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryInListViewModel>>> GetAllCategories()
        {
            var result = await _mediator.Send(new GetCategoriesListQuery());

            if (result.Status == ResponseStatus.BussinesLogicError)
                return Forbid(result.Message);
            if (result.Status == ResponseStatus.NotFoundInDataBase)
                return NotFound(result.Message);
            if (result.Status == ResponseStatus.ValidationError)
                return BadRequest(result.Message);
            if (result.Status == ResponseStatus.BadQuery)
                return BadRequest(result.Message);
            if (!result.Success)
                return MethodFailure(result.Message);

            return Ok(result.List);
        }

        [HttpGet("byId/{id}", Name = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(420)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var result = await _mediator.Send
                (new GetCategoryQuery()
                { CategoryId = new CategoryId(id) });

            if (result.Status == ResponseStatus.BussinesLogicError)
                return Forbid(result.Message);
            if (result.Status == ResponseStatus.NotFoundInDataBase)
                return NotFound(result.Message);
            if (result.Status == ResponseStatus.ValidationError)
                return BadRequest(result.Message);
            if (result.Status == ResponseStatus.BadQuery)
                return BadRequest(result.Message);
            if (!result.Success)
                return MethodFailure(result.Message);

            return Ok(result.Category);
        }

        [HttpGet("byuniqueid/{uid}", Name = "GetCategoryByUniqueId")]
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
                { CategoryUniqueId = new CategoryUniqueId(uid) });

            if (result.Status == ResponseStatus.BussinesLogicError)
                return Forbid(result.Message);
            if (result.Status == ResponseStatus.NotFoundInDataBase)
                return NotFound(result.Message);
            if (result.Status == ResponseStatus.ValidationError)
                return BadRequest(result.Message);
            if (result.Status == ResponseStatus.BadQuery)
                return BadRequest(result.Message);
            if (!result.Success)
                return MethodFailure(result.Message);

            return Ok(result.Category);
        }

        [HttpPut("byId", Name = "updateByIdcategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateById([FromBody] UpdateCategoryById u)
        {
            UpdateCategoryCommand updateCommand = new UpdateCategoryCommand()
            {
                DisplayName = u.DisplayName,
                Id = u.Id,
                Name = u.Name,
                WhatWeAreLookingFor = u.WhatWeAreLookingFor
            };

            var result = await _mediator.Send(updateCommand);



            return NoContent();
        }

        [HttpPut("ByUniqueId", Name = "updateByUniqueIdcategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateByUniqueId([FromBody] UpdateCategoryByUniqueId u)
        {
            UpdateCategoryCommand updateCommand = new UpdateCategoryCommand()
            {
                DisplayName = u.DisplayName,
                UniqueId = u.UniqueId,
                Name = u.Name,
                WhatWeAreLookingFor = u.WhatWeAreLookingFor
            };

            var result = await _mediator.Send(updateCommand);

            return NoContent();
        }

        [HttpPost(Name = "addcategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(420)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IdsDto>> Create
            ([FromBody] CreatedCategoryCommand createCategoryCommand)
        {
            var result = await _mediator.Send(createCategoryCommand);

            if (result.Status == ResponseStatus.BussinesLogicError)
                return Forbid(result.Message);
            if (result.Status == ResponseStatus.NotFoundInDataBase)
                return NotFound(result.Message);
            if (result.Status == ResponseStatus.ValidationError)
                return BadRequest(result.Message);
            if (result.Status == ResponseStatus.BadQuery)
                return BadRequest(result.Message);
            if (!result.Success)
                return MethodFailure(result.Message);

            return Ok(result.CategoryIds);
        }
    }
}
