namespace PocztaDesktop.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}
    private void OnChangeThemeToDark(object sender, EventArgs e)
    {
        ((App)Application.Current).SetTheme("DarkTheme");
    }

    private void OnChangeThemeToLight(object sender, EventArgs e)
    {
        ((App)Application.Current).SetTheme("LightTheme");
    }

}