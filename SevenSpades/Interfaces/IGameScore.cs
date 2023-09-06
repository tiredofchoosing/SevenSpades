namespace SevenSpades.Interfaces
{
    public interface IGameScore
    {
        public IScoreList Bid { get; }
        public IScoreList Fact { get; }
        public IScoreList Result { get; }
        public IList<int> GetScoresForLastStep();
        public IList<int> GetScoresForStep(int step);

        /// <summary>
        /// Get player(s) with higest score.
        /// </summary>
        /// <returns>Returns <see cref="IList"/> with 1 or more <see cref="IPlayer"/> with higest score</returns>
        public IList<IPlayer> GetWinner();

        /// <summary>
        /// Add new bids and facts to scores and calculate Result set.
        /// </summary>
        /// <param name="state">Game state to validate bids and facts</param>
        public void Add(IList<int> bid, IList<int> fact, IGameState state);
        public IScoreTable GetScoreTable();

    }
    public interface IScoreList : IList<IList<int>>
    {

    }

    public interface IScoreTable
    {
        // TODO
    }
}
