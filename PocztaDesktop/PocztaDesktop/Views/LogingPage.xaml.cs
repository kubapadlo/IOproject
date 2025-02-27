namespace PocztaDesktop.Views;

using Microsoft.Extensions.Logging.Abstractions;
using PocztaDesktop.EndPoint;
using PocztaDesktop.ViewModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;

public partial class LogingPage : ContentPage
{
	public LogingPage()
	{
		InitializeComponent();
        //BindingContext = new MainViewModel();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var username = LoginEntry.Text;
        var password = PasswordEntry.Text;

        var wpisane_dane = new { Username = username, Password = password };
        try
        {
            using(HttpClient _client = new HttpClient())
            {
                var json = JsonSerializer.Serialize(wpisane_dane);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("http://localhost:5118/api/Items/login", content);
   

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<LoginResponse>(result, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    Preferences.Set("AuthToken", responseObject.Token);
                    await DisplayAlert("Sukces", "Zalogowano pomyœlnie", "OK");
                    LoginEntry.Text = "";
                    PasswordEntry.Text = "";
                    Shell.Current.GoToAsync("AdminMainPage");
                }
                else
                {
                    MessageLabel.Text = "B³êdny login lub has³o";
                    MessageLabel.IsVisible = true;
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }

    }
    public class LoginResponse
    {
        public string Token { get; set; }
    }
}