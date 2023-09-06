namespace SevenSpadesApp.Controls;

public partial class PlayerInitInfo : ContentView
{
    private readonly int playerId;

    public int PlayerId => playerId;
    public string PlayerName => playerNameEntry.Text ?? "";

    public PlayerInitInfo(int id)
    {
        playerId = id;

        InitializeComponent();
        mainGrid.Padding = new Thickness(0, 4, 0, 4);
        playerIdLbl.Padding = new Thickness(0, 4, 20, 0);

        playerIdLbl.Text = id.ToString();
        playerNameEntry.Placeholder = $"Player {id}";
    }

    public void RaiseErrorOnEntry()
    {
        playerNameEntry.PlaceholderColor = Colors.Red;
        playerNameEntry.TextColor = Colors.Red;
    }
}