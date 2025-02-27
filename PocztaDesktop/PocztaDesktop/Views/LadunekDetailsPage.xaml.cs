using PocztaDesktop.Model;
using System.Text.Json;

namespace PocztaDesktop.Views;

public partial class LadunekDetailsPage : ContentPage
{
    private Ladunek _ladunek;
	public LadunekDetailsPage(Ladunek ladunek)
	{
		InitializeComponent();
        _ladunek = ladunek;
        LoadDetails();
    }

    private async void LoadDetails()
    {
        try
        {
            using (HttpClient _client = new HttpClient())
            {
                var token = Preferences.Get("AuthToken", string.Empty);

                if (string.IsNullOrWhiteSpace(token))
                {
                    await DisplayAlert("Error", "You are not authorized to perform this action.", "OK");
                    return;
                }

                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = $"http://localhost:5118/api/Items/show_ladunek/{_ladunek.IdLadunek}";
                var response = await _client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var ladunekDetails = JsonSerializer.Deserialize<LadunekDetailsDto>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // Wyœwietlanie szczegó³ów ³adunku na stronie
                    SamochodLabel.Text = ladunekDetails.IdSamochodu.ToString();
                    StatusLabel.Text = ladunekDetails.Status;

                }
                else
                {
                    await DisplayAlert("B³¹d", "Nie uda³o siê pobraæ szczegó³ów ³adunku.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", $"Wyst¹pi³ problem: {ex.Message}", "OK");
        }
    }
}