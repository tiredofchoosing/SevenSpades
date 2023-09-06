namespace SevenSpades.Common
{
    public static class Game
    {
        public static int MaxPlayers => 6;
        public static int MinPlayers => 2;
        public static int MaxCardsToDeal => 6;
        public static int RepeatRatio => 1;
        public static int GetMaxStepCount(int playersCount, GameMode gameMode)
        {
            return gameMode switch
            {
                GameMode.Normal => playersCount * RepeatRatio + (MaxCardsToDeal - 1) * 2,
                GameMode.NoTrumpCards => playersCount * RepeatRatio,
                GameMode.Blindly => playersCount * RepeatRatio,
                _ => throw new ArgumentException("No such game mode")
            };
        }

        public enum GameMode
        {
            Normal,
            NoTrumpCards,
            Blindly
        }

        public static class ScorePonts
        {
            public static int Normal => 10;
            public static int Skip => 5;
            public static int Overdo => 1;
            public static int Shortfall => -10;
        }
    }
}
