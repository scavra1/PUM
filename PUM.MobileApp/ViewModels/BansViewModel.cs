namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using PUM.MobileApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class BansViewModel : ViewModelBase
    {
        public BansViewModel()
        {
            BansCollection = new ObservableCollection<Ban>();
            DownloadBans();
        }

        public ObservableCollection<Ban> BansCollection { get; private set; }


        private async Task DownloadBans()
        {
            var uri= new Uri("http://localhost");
            var client = new HttpClient();
            var response = await client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();
            var bans = JsonConvert.DeserializeObject<List<Ban>>(content);

            BansCollection = new ObservableCollection<Ban>(bans);
        }
    }
}
