﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Src.Features.User;

namespace Src.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid userId)
        {
            var getUser = new GetUser.Query
            {
                Id = userId
            };

            var result = await _mediator.Send(getUser);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var claims = HttpContext.User.Claims;
            var extra = HttpContext.User.Identity;
            var q = User.Claims.Select(c => new {c.Type, c.Value}).ToList();

            var userId = string.Empty;

            var getUser = new GetUser.Query
            {
                Id = Guid.Parse(userId)
            };

            var result = await _mediator.Send(getUser);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetUser([FromBody] CreateUser.Command user)
        {
            var result = await _mediator.Send(user);

            return Accepted(result);
        }
    }
}