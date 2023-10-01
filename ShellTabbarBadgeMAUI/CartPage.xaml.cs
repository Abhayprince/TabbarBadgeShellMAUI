namespace ShellTabbarBadgeMAUI;

public partial class CartPage : ContentPage
{
    public CartPage()
    {
        InitializeComponent();
    }

    void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
    {
        BadgeCounterService.SetCount(0);
    }
}
