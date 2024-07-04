using MediatR;
using SlotGameServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsDetails
{
    public class GetGameBetsDetailsQuery : IRequest<GameBetEntity>
    {
        public int Id { get; }

        public GetGameBetsDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
