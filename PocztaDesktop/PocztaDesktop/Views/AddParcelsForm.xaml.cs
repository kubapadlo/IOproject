using PocztaDesktop.Model;
using System.Net.Http.Json;

namespace PocztaDesktop.Views;

public partial class AddParcelsForm : ContentPage
{
	public AddParcelsForm()
	{
		InitializeComponent();
	}
    private async void SaveParcelButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var newParcel = new Paczki
            {
                Gabaryt = GabarytSwitch.IsToggled,
                KodPocztowyNadawcy = SenderPostalCodeEntry.Text,
                KodPocztowyOdbiorcy = RecipientPostalCodeEntry.Text,
                DaneNadawcy = SenderDetailsEntry.Text,
                DaneOdbiorcy = RecipientDetailsEntry.Text,
                AdresNadawcy = SenderAddressEntry.Text,
                AdresOdbiorcy = RecipientAddressEntry.Text,
                Status = StatusPicker.SelectedItem?.ToString(),
                CzyZniszczona = IsDamagedSwitch.IsToggled
            };

            using (HttpClient _client = new HttpClient())
            {
                var token = Preferences.Get("AuthToken", string.Empty);

                if (string.IsNullOrWhiteSpace(token))
                {
                    await DisplayAlert("B³¹d", "Nie masz uprawnieñ, aby wykonaæ tê akcjê.", "OK");
                    return;
                }

                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = "http://localhost:5118/api/Items/add_parcel";
                var response = await _client.PostAsJsonAsync(apiUrl, newParcel);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Sukces", "Przesy³ka zosta³a dodana pomyœlnie.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("B³¹d", "Nie uda³o siê dodaæ przesy³ki. Spróbuj ponownie.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", $"Wyst¹pi³ problem: {ex.Message}", "OK");
        }
    }
}