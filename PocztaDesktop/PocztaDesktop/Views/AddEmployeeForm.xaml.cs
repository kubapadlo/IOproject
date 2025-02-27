using Microsoft.Extensions.Logging.Abstractions;
using PocztaDesktop.Model;
using System.Net.Http.Json;

namespace PocztaDesktop.Views;

public partial class AddEmployeeForm : ContentPage
{
	public AddEmployeeForm()
	{
		InitializeComponent();
	}
    private async void SaveEmployeeButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var newEmployee = new Employee
            {
                Imiê = NameEntry.Text,
                Nazwisko = SurnameEntry.Text,
                Adres = AddressEntry.Text,
                NrTelefonu = PhoneEntry.Text,
                Login = LoginEntry.Text,
                Has³o = PasswordEntry.Text,
                IdUprawnienia = int.TryParse(PermissionIdEntry.Text, out var id) ? id : 0
            };

            using (HttpClient _client = new HttpClient())
            {
                var token = Preferences.Get("AuthToken", string.Empty);

                if (string.IsNullOrWhiteSpace(token))
                {
                    await DisplayAlert("B³¹d", "Nie masz uprawnieñ, aby wykonaæ tê akcjê.", "OK");
                    return;
                }

                // Dodaj nag³ówek Authorization
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var apiUrl = "http://localhost:5118/api/Items/add-user";
                var response = await _client.PostAsJsonAsync(apiUrl, newEmployee);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Sukces", "U¿ytkownik zosta³ dodany pomyœlnie.", "OK");
                    await Navigation.PopAsync(); // Powrót do poprzedniej strony
                }
                else
                {
                    await DisplayAlert("B³¹d", "Nie uda³o siê dodaæ u¿ytkownika. Spróbuj ponownie.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", $"Wyst¹pi³ problem: {ex.Message}", "OK");
        }
    }
}