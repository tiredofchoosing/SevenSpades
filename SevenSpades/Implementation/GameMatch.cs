using SevenSpades.Common;
using SevenSpades.Interfaces;

namespace SevenSpades.Implementation
{
    public class GameMatch : IGameMatch
    {
        public IList<IPlayer> PlayersList { get; init; }
        public Game.GameMode GameMode { get; init; }
        public IGameScore Score { get; init; }
        public IGameState CurrentState { get; init; }
        public int PlayersCount => PlayersList.Count;

        public GameMatch(IList<IPlayer> playersList, Game.GameMode gameMode)
        {
            if (playersList.Count < Game.MinPlayers)
                throw new Exception("Players count is less than minimum possible");

            PlayersList = playersList;
            GameMode = gameMode;
            Score = new GameScore(playersList, gameMode);
            CurrentState = new GameState(playersList, gameMode);
        }
    }
}
