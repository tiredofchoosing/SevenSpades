using SevenSpades.Interfaces;
using SevenSpades.Helpers;
using SevenSpades.Common;

namespace SevenSpades.Implementation
{
    class GameScore : IGameScore
    {
        private readonly IList<IPlayer> playersList;
        private readonly Game.GameMode gameMode;
        private readonly int playersCount;

        public IScoreList Bid { get; init; }
        public IScoreList Fact { get; init; }
        public IScoreList Result { get; init; }

        public IList<int> GetScoresForLastStep()
        {
            CheckResultList();
            return Result.Last();
        }

        public IList<int> GetScoresForStep(int step)
        {
            CheckResultList();

            if (step <= 0 || step > Result.Count)
                throw new ArgumentException($"Invalid step value: {step}");

            return Result[step - 1];
        }

        public IList<IPlayer> GetWinner()
        {
            CheckResultList();

            int max = Result.Last().Max();
            var ids = Result.Last().IndexesOf(max);
            return ids.Select(e => playersList[e]).ToList();
        }

        public void Add(IList<int> bid, IList<int> fact, IGameState state)
        {
            if (!ValidateBid(bid, state))
                throw new ArgumentException("Bid list is invalid", nameof(bid));
            if (!ValidateFact(fact, state))
                throw new ArgumentException("Fact list is invalid", nameof(fact));

            Bid.Add(bid);
            Fact.Add(fact);

            var result = new List<int>(playersCount);
            var lastResult = Result.LastOrDefault();
            if (lastResult == null)
                lastResult = Enumerable.Repeat(0, playersCount).ToList();

            foreach (var (b, f, l) in bid.Zip(fact,lastResult))
            {
                int r = 0;
                if (b == f)
                {
                    if (b == 0)
                        r = Game.ScorePonts.Skip;
                    else
                        r = Game.ScorePonts.Normal * f;
                }
                else if (b < f)
                {
                    r = Game.ScorePonts.Overdo * f;
                }
                else
                {
                    r = Game.ScorePonts.Shortfall * (b - f);
                }
                result.Add(r + l);
            }

            Result.Add(result);
        }

        public IScoreTable GetScoreTable()
        {
            throw new NotImplementedException();
            // TODO
        }

        public GameScore(IList<IPlayer> _playersList, Game.GameMode _gameMode)
        {
            playersList = _playersList;
            playersCount = _playersList.Count;
            gameMode = _gameMode;

            int count = Game.GetMaxStepCount(playersCount, gameMode);

            Bid = new ScoreList(count);
            Fact = new ScoreList(count);
            Result = new ScoreList(count);
        }

        private void CheckResultList()
        {
            if (!Result.Any())
                throw new Exception("Result list is empty");
        }

        private bool ValidateBid(IList<int> bid, IGameState state)
        {
            if (bid.Count != playersCount)
                return false;

            if (bid.Max() > state.CurrentCardCount)
                return false;

            if (bid.Sum() == state.CurrentCardCount)
                return false;

            return true;
        }

        private bool ValidateFact(IList<int> fact, IGameState state)
        {
            if (fact.Count != playersCount)
                return false;

            if (fact.Max() > state.CurrentCardCount)
                return false;

            if (fact.Sum() != state.CurrentCardCount)
                return false;

            return true;
        }
    }

    class ScoreList : List<IList<int>>, IScoreList
    {
        public ScoreList() : base() { }
        public ScoreList(int capacity) : base(capacity) { }
    }

}
