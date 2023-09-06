using System.Collections;
using SevenSpades.Common;

namespace SevenSpadesApp.Pages;

public partial class MainPage : ContentPage
{
    private readonly IList modes;
    private int playersCount;

    public int PlayersCountProp
    {
        get { return playersCount; }
        set
        {
            if (value >= Game.MinPlayers && value <= Game.MaxPlayers)
            {
                playersCount = value;
                OnPropertyChanged();
            }
        }
    }

	public MainPage()
	{
        modes = new string[] { "Стандартный", "Без козырей", "Вслепую" };

		InitializeComponent();

        playersCountLbl.BindingContext = this;
        playersCountLbl.SetBinding(Label.TextProperty, "PlayersCountProp");
        PlayersCountProp = Game.MinPlayers;

        gameModesPicker.ItemsSource = modes;
        gameModesPicker.SelectedIndex = 0;
	}

    private void OnPlayersDecBtn_Clicked(object sender, EventArgs e)
	{
        PlayersCountProp--;
    }
    private void OnPlayersIncBtn_Clicked(object sender, EventArgs e)
    {
        PlayersCountProp++;
    }

    private void OnStartBtn_Clicked(object sender, EventArgs e)
    {
        Game.GameMode mode = (Game.GameMode)gameModesPicker.SelectedIndex;
        var initPage = new InitPage(PlayersCountProp, mode);
        Navigation.PushAsync(initPage);
    }
}