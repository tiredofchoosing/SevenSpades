using SevenSpades.Interfaces;

namespace SevenSpadesApp.Controls;

public partial class PlayerBidInfo : ContentView
{
    public string Bid => bidEntry.Text ?? "";
    public string Fact => factEntry.Text ?? "";
	public IPlayer Player { get; init; }

    public PlayerBidInfo(IPlayer player)
	{
		Player = player;

		InitializeComponent();
		playerNameLbl.Text = player.Name;
        mainGrid.Padding = new Thickness(0, 4, 0, 4);
        playerNameLbl.Padding = new Thickness(0, 4, 20, 0);

        ClearEntries();
    }

	public void ClearEntries()
	{
		bidEntry.Text = "";
		factEntry.Text = "";

        bidEntry.PlaceholderColor = Colors.DarkGrey;
        bidEntry.TextColor = Colors.White;

        factEntry.PlaceholderColor = Colors.DarkGrey;
        factEntry.TextColor = Colors.White;
    }

    public void RaiseErrorOnBidEntry()
    {
        bidEntry.PlaceholderColor = Colors.Red;
        bidEntry.TextColor = Colors.Red;
    }
    public void RaiseErrorOnFactEntry()
    {
        factEntry.PlaceholderColor = Colors.Red;
        factEntry.TextColor = Colors.Red;
    }
}