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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

       
        [HttpPost("start")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> StartGame(StartGameCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { result.SessionId });
        }

        
        [HttpPost("spins")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<SpinResult>> Spin([FromBody] SpinCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<GetGameBetsListModel>>> GetAll([FromQuery] GameBetsListQueryParameter parameter)
        {
            var query = new GetAllGameBetsQuery(parameter.PageNumber, parameter.PageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

     
        [HttpGet("{id}")]
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