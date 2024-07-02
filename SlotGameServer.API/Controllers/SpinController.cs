using MediatR;
using Microsoft.AspNetCore.Mvc;
using SlotGameServer.Application.Spin.Commands.Games;

namespace SlotGameServer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpinController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpinController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("spin")]
        public async Task<IActionResult> Spin([FromBody] SpinCommand command)
        {
            //if (command.BetAmount <= 0)
            //{
            //    return BadRequest("Bet amount must be greater than zero.");
            //}

            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}