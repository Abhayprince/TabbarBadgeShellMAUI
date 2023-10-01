namespace ShellTabbarBadgeMAUI;

public partial class MainPage : ContentPage, IDisposable
{

    public MainPage()
    {
        InitializeComponent();
        BadgeCounterService.CountChanged += OnCountChanged;
    }

    private void OnCountChanged(object? sender, int newCount)
    {
        counterLabel.Text = $"Current Badge Count: {newCount}";
    }

    private void IncreaseBadgeCountClicked(object sender, EventArgs e)
    {
        BadgeCounterService.SetCount(BadgeCounterService.Count + 1);
    }

    private void DecreaseBadgeCountClicked(object sender, EventArgs e)
    {
        BadgeCounterService.SetCount(BadgeCounterService.Count - 1);
    }

    public void Dispose() => BadgeCounterService.CountChanged -= OnCountChanged;
}


