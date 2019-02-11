namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using PUM.SharedModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Windows.UI.Xaml;

    public class BansViewModel : ViewModelBase
    {
        public BansViewModel()
        {
            DownloadBans();
        }

        public ObservableCollection<Ban> BansCollection
        {
            get
            {
                return bansCollection;
            }
            set
            {
                bansCollection = value;
                RaisePropertyChanged("BansCollection");
            }
        }


        private bool isWorking;
        public bool IsWorking
        {
            get
            {
                return isWorking;
            }
            set
            {
                if (isWorking != value)
                {
                    isWorking = value;
                    RaisePropertyChanged("IsWorking");
                }
            }
        }

        private async Task DownloadBans()
        {
            IsWorking = true;

            var uri = new Uri("http://localhost/api/bans/getbans");
            var client = new HttpClient();
            var response = await client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();
            var bans = JsonConvert.DeserializeObject<List<Ban>>(content);

            BansCollection = new ObservableCollection<Ban>(bans);

            IsWorking = false;
        }

        private ObservableCollection<Ban> bansCollection;
    }
}
