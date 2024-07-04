using System.ComponentModel;


namespace SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsList
{
    public class GameBetsListQueryParameter
    {
        [DefaultValue(1)]
        public int PageNumber { get; set; }

        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}
