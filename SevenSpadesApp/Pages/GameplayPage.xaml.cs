using SevenSpades.Common;
using SevenSpades.Implementation;
using SevenSpades.Interfaces;
using SevenSpadesApp.Controls;
using System.Diagnostics;

namespace SevenSpadesApp.Pages;

public partial class GameplayPage : ContentPage
{
    private readonly IGameMatch gameMatch;
    private readonly int playersCount;
    private bool gameFinished = false;

    public GameplayPage(IGameMatch _gameMatch)
	{
        gameMatch = _gameMatch;
        playersCount = gameMatch.PlayersList.Count;

        InitializeComponent();

        foreach (IPlayer player in gameMatch.PlayersList)
        {
            var pbi = new PlayerBidInfo(player);
            playersBidInfo.Add(pbi);
        }

        UpdatePage();
        SwitchButtons(true, false);
    }

    private void OnNextDealBtn_Clicked(object sender, EventArgs e)
    {
        if (gameFinished)
            return;

        var bid = new List<int>(playersCount);
        var fact = new List<int>(playersCount);
        foreach (PlayerBidInfo pbi in playersBidInfo.Children)
        {
            bool bidFlag = int.TryParse(pbi.Bid, out int b);
            bool factFlag = int.TryParse(pbi.Fact, out int f);

            if (bidFlag && factFlag)
            {
                bid.Add(b);
                fact.Add(f);
            }
            else
            {
                if (!bidFlag)
                {
                    pbi.RaiseErrorOnBidEntry();
                }
                if (!factFlag)
                {
                    pbi.RaiseErrorOnFactEntry();
                }
                return;
            }
        }

        try
        {
            gameMatch.Score.Add(bid, fact, gameMatch.CurrentState);

            var step = gameMatch.CurrentState.CurrentStep;
            if (step == Game.GetMaxStepCount(playersCount, gameMatch.GameMode))
            {
                gameFinished = true;
                SwitchButtons(false, true);
            }
            if (!gameFinished)
            {
                gameMatch.CurrentState.NextStep();
                UpdatePage();
            }
        }
        catch (ArgumentException ex)
        {
            // TODO
            if (ex.ParamName == "bid")
            {
                Debug.WriteLine(ex.Message);
            }
            else if (ex.ParamName == "fact")
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    private void OnResultBtn_Clicked(object sender, EventArgs e)
    {
        if (!gameFinished)
            return;

        var resultPage = new ResultsPage(gameMatch.Score);
        Navigation.PushAsync(resultPage);
    }

    private void UpdatePage()
    {
        var state = gameMatch.CurrentState;

        dealCountLbl.Text = state.CurrentStep.ToString();
        cardNumberLbl.Text = state.CurrentCardCount.ToString();

        foreach (PlayerBidInfo pbi in playersBidInfo.Children)
        {
            pbi.ClearEntries();

            if (pbi.Player == state.CurrentHost)
            {
                pbi.Focus();
                pbi.BackgroundColor = Colors.LightGreen.WithAlpha(0.5f);
            }
            else
                pbi.BackgroundColor = Colors.Transparent;
        }
    }

    private void SwitchButtons(bool next, bool finish)
    {
        nextDealBtn.IsEnabled = next;
        resultBtn.IsEnabled = finish;
    }
}