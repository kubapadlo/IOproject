namespace PocztaDesktop.Views;

public partial class WarehouseMainPage : ContentPage
{
	public WarehouseMainPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("LadunkiPage");
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("ParcelsPage");
    }
}