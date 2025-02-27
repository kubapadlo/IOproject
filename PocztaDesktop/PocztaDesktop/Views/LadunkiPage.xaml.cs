using System.Text.Json;
using PocztaDesktop.Model;

namespace PocztaDesktop.Views;

public partial class LadunkiPage : ContentPage
{
	public LadunkiPage()
	{
		InitializeComponent();
        StartAutoRefresh();
    }

    private async void LoadLadunkiButton_Clicked(object sender, EventArgs e)
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

                // Dodaj nag³ówek Authorization
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = "http://localhost:5118/api/Items/show_ladunki"; // Zaktualizowany URL API
                var response = await _client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var ladunki = JsonSerializer.Deserialize<List<Ladunek>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    /* Pobierz liczbê paczek dla ka¿dego ³adunku
                    foreach (var ladunek in ladunki)
                    {
                        var paczkiResponse = await _client.GetAsync($"http://localhost:5118/api/Items/get_paczki_count/{ladunek.IdLadunek}");
                        if (paczkiResponse.IsSuccessStatusCode)
                        {
                            var countJson = await paczkiResponse.Content.ReadAsStringAsync();
                            ladunek.LiczbaPaczek = int.Parse(countJson);
                        }
                    }
                    */
                    ladunkiListView.ItemsSource = ladunki;
                }
                else
                {
                    await DisplayAlert("B³¹d", "Nie uda³o siê pobraæ danych z serwera.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", $"Wyst¹pi³ problem: {ex.Message}", "OK");
        }
    }

    private async Task RefreshPaczkiCountsAsync()
    {
        try
        {
            using (HttpClient _client = new HttpClient())
            {
                var token = Preferences.Get("AuthToken", string.Empty);

                if (string.IsNullOrWhiteSpace(token))
                {
                    return; // Nie wykonuj odœwie¿ania, jeœli brak tokena
                }

                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                if (ladunkiListView.ItemsSource is List<Ladunek> ladunki)
                {
                    foreach (var ladunek in ladunki)
                    {
                        var apiUrl = $"http://localhost:5118/api/Items/get_paczki_count/{ladunek.IdLadunek}";
                        var response = await _client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            var countJson = await response.Content.ReadAsStringAsync();
                            ladunek.LiczbaPaczek = int.Parse(countJson);
                        }
                    }

                    // Odœwie¿ widok listy
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ladunkiListView.ItemsSource = null;
                        ladunkiListView.ItemsSource = ladunki;
                    });
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", $"Wyst¹pi³ problem: {ex.Message}", "OK");
        }
    }

    private void StartAutoRefresh()
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        Task.Run(async () =>
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                await RefreshPaczkiCountsAsync();
                await Task.Delay(5000); // Co 5 sekund
            }
        }, cancellationTokenSource.Token);

        // Anulowanie odœwie¿ania podczas zamykania strony
        this.Disappearing += (s, e) => cancellationTokenSource.Cancel();
    }

    private async void OnLadunekSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Ladunek ladunek)
        {
            await Navigation.PushAsync(new LadunekDetailsPage(ladunek));
        }
    }
}