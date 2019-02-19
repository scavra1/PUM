namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using PUM.MobileApp.Commands;
    using PUM.SharedModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Input;

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

        private ICommand unbanCommand;
        public ICommand UnbanCommand
        {
            get
            {
                if (unbanCommand == null)
                    unbanCommand = new UnbanCommand(this);

                return unbanCommand;
            }
        }

        private ICommand editBanCommand;
        public ICommand EditBanCommand
        {
            get
            {
                if (editBanCommand == null)
                    editBanCommand = new EditBanCommand();

                return editBanCommand;
            }
        }

        private void CreateEditDialog()
        {

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
