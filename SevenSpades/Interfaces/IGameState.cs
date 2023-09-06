namespace SevenSpades.Interfaces
{
    public interface IGameState
    {
        public IPlayer CurrentHost { get; }
        public int CurrentStep { get; }
        public int CurrentCardCount { get; }
        public void NextStep();
    }
}
