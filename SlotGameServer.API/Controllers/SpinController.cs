using MediatR;
using Microsoft.AspNetCore.Mvc;
using SlotGameServer.Application.Spin.Commands.Games;
using SlotGameServer.Application.Spin.Commands.Sessions;

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

        [HttpPost("start")]
        public async Task<IActionResult> StartGame([FromBody] StartGameCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { result.SessionId });
        }

        [HttpPost("spin")]
        public async Task<IActionResult> Spin([FromBody] SpinCommandModel model)
        {
            if (model.BetAmount <= 0)
            {
                return BadRequest("Bet amount must be greater than zero.");
            }

            var command = new SpinCommand 
            { 
                BetAmount = model.BetAmount,
                ChosenNumber = model.ChosenNumber,
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}