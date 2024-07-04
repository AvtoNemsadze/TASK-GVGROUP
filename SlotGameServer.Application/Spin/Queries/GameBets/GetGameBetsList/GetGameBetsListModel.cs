using AutoMapper;
using SlotGameServer.Domain.Entities;
using System;


namespace SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsList
{
    public class GetGameBetsListModel
    {
        public int SessionId { get; set; }
        public decimal BetAmount { get; set; }
        public int ChosenNumber { get; set; }
        public int ResultNumber { get; set; }
        public bool IsWin { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameBetEntity, GetGameBetsListModel>();
        }
    }

}
