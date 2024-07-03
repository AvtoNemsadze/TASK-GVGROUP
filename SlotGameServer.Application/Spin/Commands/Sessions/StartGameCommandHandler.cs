using Microsoft.EntityFrameworkCore;
using SlotGameServer.Domain.Entities;
using System;

namespace SlotGameServer.Application.Spin.Commands.Sessions
{
    public class StartGameCommandHandler 
    {
        public async Task<StartGameResult> Handle(StartGameCommand request, CancellationToken cancellationToken)
        {
            var newSession = new GameSessionEntity();  // Assuming defaults or DbContext logic will set necessary properties

            //_context.GameSessions.Add(newSession);
            //await _context.SaveChangesAsync(cancellationToken);

            return new StartGameResult { SessionId = newSession.Id };
        }

    }
}
