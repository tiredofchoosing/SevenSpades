using SevenSpades.Common;
using SevenSpades.Interfaces;

namespace SevenSpades.Implementation
{
    class GameState : IGameState
    {
        private int curStep, playersCount;
        private IList<IPlayer> playersList;
        private Game.GameMode gameMode;

        public IPlayer CurrentHost { get; private set; }
        public int CurrentStep
        {
            get => curStep;
            private set
            {
                if (value > Game.GetMaxStepCount(playersCount, gameMode))
                    throw new Exception("CurrentStep is more than maximum possible");

                if (value <= 0)
                    throw new Exception("CurrentCardCount is less or equal zero");

                curStep = value;
            }
        }
        public int CurrentCardCount
        {
            get
            {
                return gameMode switch
                {
                    Game.GameMode.Normal => GetCardCountNormal(),
                    Game.GameMode.NoTrumpCards => Game.MaxCardsToDeal,
                    Game.GameMode.Blindly => Game.MaxCardsToDeal,
                    _ => throw new ArgumentException("No such game mode")
                };
            }
        }

        public void NextStep()
        {
            CurrentStep++;
            int nextPlayerId = (CurrentStep % playersCount) - 1;
            nextPlayerId = nextPlayerId == -1 ? playersCount - 1 : nextPlayerId;
            CurrentHost = playersList[nextPlayerId];
        }

        public GameState(IList<IPlayer> _playersList, Game.GameMode _gameMode)
        {
            playersList = _playersList;
            playersCount = _playersList.Count;
            gameMode = _gameMode;
            CurrentHost = _playersList.First();
            CurrentStep = 1;
        }

        private int GetCardCountNormal()
        {
            if (CurrentStep < Game.MaxCardsToDeal)
                return CurrentStep;

            if (CurrentStep < Game.MaxCardsToDeal + playersCount * Game.RepeatRatio)
                return Game.MaxCardsToDeal;

            int count = Game.MaxCardsToDeal - (CurrentStep - (Game.MaxCardsToDeal + playersCount * Game.RepeatRatio)) - 1;

            if (count <= 0)
                throw new Exception("CurrentCardCount is less or equal zero");

            return count;
        }
    }
}
