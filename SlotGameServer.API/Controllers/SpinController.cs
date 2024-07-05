using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlotGameServer.API.AuthConfig;
using SlotGameServer.Application.Spin.Commands.Games;
using SlotGameServer.Application.Spin.Commands.Sessions;
using SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsDetails;
using SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsList;

namespace SlotGameServer.API.Controllers
{
    [ApiController]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class SpinController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SpinController
            (IMediator mediator,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Initiates a new game session and returns the session ID.
        /// </summary>
        /// <param name="command">The command containing necessary information to start a new game session.</param>
        /// <returns>Returns a newly created game session ID.</returns>
        [HttpPost("start")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StartGame(StartGameCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { result.SessionId });
        }

        /// <summary>
        /// Processes a spin in the casino game, checking user's balance, performing the spin, and updating user statistics.
        /// </summary>
        /// <param name="model">Spin command parameters including bet amount, chosen number, and session ID.</param>
        /// <returns>Returns the result of the spin including win/loss information and any winnings.</returns>
        [HttpPost("spin")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpinResult))]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Spin([FromBody] SpinCommandModel model)
        {
            var validator = new SpinCommandModelValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                });
                return BadRequest(errors);
            }

            var currentUserId = ClaimsPrincipalExtensions.GetUserId(_httpContextAccessor);

            var command = new SpinCommand
            {
                BetAmount = model.BetAmount,
                ChosenNumber = model.ChosenNumber,
                SessionId = model.SessionId,
                CreateUserId = currentUserId.GetValueOrDefault(),
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all game bets with pagination.
        /// </summary>
        /// <param name="parameter">The pagination parameters.</param>
        /// <returns>A paginated list of game bet entities.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<GetGameBetsListModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedResponse<GetGameBetsListModel>>> GetAll([FromQuery] GameBetsListQueryParameter parameter)
        {
            var query = new GetAllGameBetsQuery(parameter.PageNumber, parameter.PageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a game bet by its ID.
        /// </summary>
        /// <param name="id">The ID of the game bet to retrieve.</param>
        /// <returns>The game bet entity with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetGameBetsDetailsQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}