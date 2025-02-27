using Microsoft.Maui.Controls;

namespace PocztaDesktop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetTheme("LightTheme");
            MainPage = new AppShell();
        }
        public void SetTheme(string themeKey)
        {
            try
            {
                if (themeKey == "DarkTheme")
                {
                    Resources["BackgroundColor"] = Resources["DarkBackgroundColor"];
                    Resources["TextColor"] = Resources["DarkTextColor"];
                    Resources["PlaceholderColor"] = Resources["DarkPlaceholderText"];
                    Resources["ButtonColor"] = Resources["DarkButtonBackground"];
                    Resources["ButtonTextColor"] = Resources["DarkButtonText"];
                    Resources["PlaceholderBackground"] = Resources["DarkPlaceholderBackground"];
                }
                else
                {
                    Resources["BackgroundColor"] = Resources["LightBackgroundColor"];
                    Resources["TextColor"] = Resources["LightTextColor"];
                    Resources["PlaceholderColor"] = Resources["LightPlaceholderText"];
                    Resources["ButtonColor"] = Resources["LightButtonBackground"];
                    Resources["ButtonTextColor"] = Resources["LightButtonText"];
                    Resources["PlaceholderBackground"] = Resources["LightPlaceholderBackground"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting theme: {ex.Message}");
            }
        }
    }
}
