using SevenSpades.Common;

namespace SevenSpades.Interfaces
{
    public interface IGameMatch
    {
        public IList<IPlayer> PlayersList { get; }
        public int PlayersCount { get; }
        public Game.GameMode GameMode { get; }
        public IGameScore Score { get; }
        public IGameState CurrentState { get; }
    }
}
