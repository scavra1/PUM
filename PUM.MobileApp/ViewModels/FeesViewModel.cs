namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using PUM.MobileApp.Commands;
    using PUM.MobileApp.Commands.CommonCommands;
    using PUM.MobileApp.Commands.FeesCommands;
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

    public class FeesViewModel : ViewModelBase, IBackableViewModel, IAppBarableViewModel, IRefreshableViewModel, IAddableViewModel, IAdminableViewModel, IFilterableViewModel
    {
        public FeesViewModel(IUserService userService)
        {
            UserService = userService;
            AdminPanelVisibility = UserService.CurrentUser.IsAdmin;
            CurrentView = "All fees";
            RefreshView();
        }

        #region Properties
        public IUserService UserService { get; private set; }

        private ICollection<Fee> feesCollection;

        private ObservableCollection<Fee> feesObservableCollection;
        public ObservableCollection<Fee> FeesObservableCollection
        {
            get
            {
                return feesObservableCollection;
            }
            set
            {
                feesObservableCollection = value;
                RaisePropertyChanged("FeesObservableCollection");
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

        #region Interfaces implementations
        public string LastFilter { get; set; }

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
        #endregion

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

        private ICommand applyFilterCommand;
        public ICommand ApplyFilterCommand
        {
            get
            {
                if (applyFilterCommand == null)
                    applyFilterCommand = new ApplyFilterCommand(this);

                return applyFilterCommand;
            }
        }
        #endregion

        private ICommand showAllFeesCommand;
        public ICommand ShowAllFeesCommand
        {
            get
            {
                if (showAllFeesCommand == null)
                    showAllFeesCommand = new ShowAllFeesCommand(this);

                return showAllFeesCommand;
            }
        }
        #endregion
        #endregion

        #region Methods
        #region Interfaces implementations
        public void FilterCollection(object parameter = null)
        {
            IsWorking = true;

            if (parameter != null)
            {
                LastFilter = (string)parameter;
                ShowAllFees();
            }

            if (LastFilter == "Mine")
            {
                FeesObservableCollection = new ObservableCollection<Fee>(FeesObservableCollection.Where(x => x.UserID == UserService.CurrentUser.UserID));
                CurrentView = "Mine fees";
            }
            else
            {
                FeesObservableCollection = new ObservableCollection<Fee>(FeesObservableCollection);
                CurrentView = "All fees";
            }

            IsWorking = false;
        }

        public void RefreshView()
        {
            Task.Run(async () => { await DownloadFees(); }).Wait();
            ShowAllFees();
        }

        public void AddItem()
        {
            throw new System.NotImplementedException();
        }
        #endregion
        public void ShowAllFees()
        {
            IsWorking = true;

            FeesObservableCollection = new ObservableCollection<Fee>(feesCollection);
            FilterCollection();

            IsWorking = false;
        }

        private async Task DownloadFees()
        {
            var uriString = "http://localhost/api/fees";
            if (UserService.CurrentUser.IsAdmin)
            {
                uriString += "/getfees";
            }
            else
            {
                uriString += "/getuserfees?userID=" + UserService.CurrentUser.UserID.ToString();
            }

            var uri = new Uri(uriString);
            var client = new HttpClient();
            var response = await client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();
            feesCollection = JsonConvert.DeserializeObject<List<Fee>>(content);
        }
        #endregion
    }
}
