using SevenSpades.Interfaces;
using System.Text;

namespace SevenSpadesApp.Pages;

public partial class ResultsPage : ContentPage
{
	private readonly IGameScore gameScore;

	public ResultsPage(IGameScore _gameScore)
	{
		// TODO Add full scores table to the page

        gameScore = _gameScore;

		InitializeComponent();

		StringBuilder sb = new();

		IList<IPlayer> players = gameScore.GetWinner();
		foreach (var player in players)
		{
			sb.AppendLine($"{player.Name} ({gameScore.GetScoresForLastStep().Max()})");
		}
		playerName.Text = sb.ToString();
	}
}