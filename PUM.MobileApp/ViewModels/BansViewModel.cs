namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using PUM.MobileApp.Commands;
    using PUM.MobileApp.Commands.BansCommands;
    using PUM.MobileApp.Commands.CommonCommands;
    using PUM.MobileApp.Services;
    using PUM.MobileApp.ViewModels.Interfaces;
    using PUM.SharedModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class BansViewModel : ViewModelBase, IBackableViewModel, IAppBarableViewModel, IRefreshableViewModel, IAddableViewModel, IAdminableViewModel
    {
        public BansViewModel(IUserService userService)
        {
            UserService = userService;
            AdminPanelVisibility = UserService.CurrentUser.IsAdmin;
            ShowAllBans();
        }

        #region Properties
        public IUserService UserService { get; private set; }

        private ICollection<Ban> bansCollection;

        private ObservableCollection<Ban> bansobservableCollection;
        public ObservableCollection<Ban> BansObservableCollection
        {
            get
            {
                return bansobservableCollection;
            }
            set
            {
                bansobservableCollection = value;
                RaisePropertyChanged("BansObservableCollection");
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

        private string currentView;
        public string CurrentView
        {
            get
            {
                return currentView;
            }
            set
            {
                currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }

        private bool adminPanelVisibility;
        public bool AdminPanelVisibility
        {
            get
            {
                return adminPanelVisibility;
            }
            set
            {
                adminPanelVisibility = value;
                RaisePropertyChanged("AdminPanelVisibility");
            }
        }

        #region Commands
        #region Interfaces implementations
        private ICommand backToMainMenuCommand;
        public ICommand BackToMainMenuCommand
        {
            get
            {
                if (backToMainMenuCommand == null)
                    backToMainMenuCommand = new NavigationCommand("MainMenu");

                return backToMainMenuCommand;
            }
        }

        private ICommand refreshViewCommand;
        public ICommand RefreshViewCommand
        {
            get
            {
                if (refreshViewCommand == null)
                    refreshViewCommand = new RefreshViewCommand(this);

                return refreshViewCommand;
            }
        }

        private ICommand addItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                if (addItemCommand == null)
                    addItemCommand = new AddItemCommand(this);

                return addItemCommand;
            }
        }
        #endregion

        private ICommand showAllBansCommand;
        public ICommand ShowAllBansCommand
        {
            get
            {
                if (showAllBansCommand == null)
                    showAllBansCommand = new ShowAllBansCommand(this);

                return showAllBansCommand;
            }
        }

        private ICommand showPastBansCommand;
        public ICommand ShowPastBansCommand
        {
            get
            {
                if (showPastBansCommand == null)
                    showPastBansCommand = new ShowPastBansCommand(this);

                return showPastBansCommand;
            }
        }

        private ICommand showActiveBansCommand;
        public ICommand ShowActiveBansCommand
        {
            get
            {
                if (showActiveBansCommand == null)
                    showActiveBansCommand = new ShowActiveBansCommand(this);

                return showActiveBansCommand;
            }
        }
        #endregion
        #endregion

        #region Methods
        #region Interfaces implementations
        public void RefreshView()
        {
            switch(currentView)
            {
                case "Past bans":
                    ShowPastBans(); break;
                case "Active bans":
                    ShowActiveBans(); break;
                default:
                    ShowAllBans(); break;
            }
        }

        public void AddItem()
        {
            throw new System.NotImplementedException();
        }
        #endregion
        public async Task ShowAllBans()
        {
            IsWorking = true;

            BansObservableCollection = new ObservableCollection<Ban>(await DownloadBans());

            CurrentView = "All bans";

            IsWorking = false;
        }

        public async Task ShowPastBans()
        {
            IsWorking = true;

            var allReservations = await DownloadBans();

            BansObservableCollection = new ObservableCollection<Ban>(allReservations.Where(x => x.Active == false));

            CurrentView = "Past bans";

            IsWorking = false;
        }

        public async Task ShowActiveBans()
        {
            IsWorking = true;

            var allReservations = await DownloadBans();

            BansObservableCollection = new ObservableCollection<Ban>(allReservations.Where(x => x.Active == true));

            CurrentView = "Active bans";

            IsWorking = false;
        }

        private async Task<ICollection<Ban>> DownloadBans()
        {
            var uriString = "http://localhost/api/bans";
            if (UserService.CurrentUser.IsAdmin)
            {
                uriString += "/getbans";
            }
            else
            {
                uriString += "/getuserbans?userID=" + UserService.CurrentUser.UserID.ToString();
            }

            var uri = new Uri(uriString);
            var client = new HttpClient();
            var response = await client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();
            bansCollection = JsonConvert.DeserializeObject<List<Ban>>(content);

            return new ObservableCollection<Ban>(bansCollection);
        }
        #endregion
    }
}
