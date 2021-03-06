﻿namespace PUM.MobileApp.ViewModels
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
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
    using Windows.UI.Xaml;

    public class BansViewModel : ViewModelBase, IBackableViewModel, IAppBarableViewModel, IRefreshableViewModel, IAddableViewModel, IAdminableViewModel, IFilterableViewModel
    {
        public BansViewModel(IUserService userService)
        {
            UserService = userService;
            AdminPanelVisibility = UserService.CurrentUser.IsAdmin;
            CurrentView = "All bans";
            LastFilter = "None";
            RefreshView();
        }

        #region Properties
        public IUserService UserService { get; private set; }

        private ICollection<Ban> bansCollection;

        private ObservableCollection<Ban> bansObservableCollection;
        public ObservableCollection<Ban> BansObservableCollection
        {
            get
            {
                return bansObservableCollection;
            }
            set
            {
                bansObservableCollection = value;
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
                    editBanCommand = new EditBanCommand(this);

                return editBanCommand;
            }
        }

        private ICommand addNewBanCommand;
        public ICommand AddNewBanCommand
        {
            get
            {
                if (addNewBanCommand == null)
                    addNewBanCommand = new AddNewBanCommand(this);

                return addNewBanCommand;
            }
        }

        #endregion
        #endregion

        #region Methods
        #region Interfaces implementations
        public void FilterCollection(object parameter = null)
        {
            IsWorking = true;

            if(parameter != null)
            {
                LastFilter = (string)parameter;
                ShowBans();
            }

            if (LastFilter == "Mine")
            {
                BansObservableCollection = new ObservableCollection<Ban>(BansObservableCollection.Where(x => x.UserID == UserService.CurrentUser.UserID));
            }
            else
            {
                BansObservableCollection = new ObservableCollection<Ban>(BansObservableCollection);
            }

            IsWorking = false;
        }

        public void RefreshView()
        {
            Task.Run(async () => { await DownloadBans(); }).Wait();
            ShowBans();
        }

        public void AddItem()
        {
            AddNewBanCommand.Execute(null);
        }
        #endregion
        private void ShowBans()
        {
            switch (currentView)
            {
                case "Past bans":
                    ShowPastBans(); break;
                case "Active bans":
                    ShowActiveBans(); break;
                default:
                    ShowAllBans(); break;
            }
        }

        public Visibility AdminView
        {
            get
            {
                var user = ServiceLocator.Current.GetInstance<IUserService>().CurrentUser;

                if(user.IsAdmin)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public void ShowAllBans()
        {
            IsWorking = true;

            BansObservableCollection = new ObservableCollection<Ban>(bansCollection);
            FilterCollection();

            CurrentView = "All bans";

            IsWorking = false;
        }

        public void ShowPastBans()
        {
            IsWorking = true;

            BansObservableCollection = new ObservableCollection<Ban>(bansCollection.Where(x => x.Active == false));
            FilterCollection();

            CurrentView = "Past bans";

            IsWorking = false;
        }

        public void ShowActiveBans()
        {
            IsWorking = true;

            BansObservableCollection = new ObservableCollection<Ban>(bansCollection.Where(x => x.Active == true));
            FilterCollection();

            CurrentView = "Active bans";

            IsWorking = false;
        }

        private async Task DownloadBans()
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
        }
        #endregion
    }
}
