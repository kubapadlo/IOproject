using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PocztaDesktop.Model;

namespace PocztaDesktop.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ObservableCollection<Paczki>> GetParcelsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Paczki>>("http://localhost:5118/api/Items/show_parcels");
            return new ObservableCollection<Paczki>(response);
        }
    }


}
