using SevenSpades.Common;
using SevenSpades.Implementation;
using SevenSpades.Interfaces;
using SevenSpadesApp.Controls;

namespace SevenSpadesApp.Pages;

public partial class InitPage : ContentPage
{
    private readonly int playersCount;
    private readonly Game.GameMode gameMode;

    public InitPage(int _playersCount, Game.GameMode _gameMode)
	{
        playersCount = _playersCount;
        gameMode = _gameMode;

		InitializeComponent();

		for (int i = 1; i <= _playersCount; i++)
		{
            var pii = new PlayerInitInfo(i);
            playersInitInfo.Add(pii);
        }
    }

    private void OnNextBtn_Clicked(object sender, EventArgs e)
    {
        var playersList = new List<IPlayer>(playersCount);

        foreach (PlayerInitInfo playerInfo in playersInitInfo.Children)
        {
            var name = playerInfo.PlayerName;
            if (string.IsNullOrWhiteSpace(name) ||
                playersList.Any(p => p.Name == name))
            {
                playerInfo.RaiseErrorOnEntry();
                return;
            }

            var player = new Player() { Name = name };
            playersList.Add(player);
        }

        var gameMatch = new GameMatch(playersList, gameMode);
        var gameplayPage = new GameplayPage(gameMatch);
        Navigation.PushAsync(gameplayPage);
    }
}