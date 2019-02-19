﻿using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using PUM.MobileApp.Commands;
using PUM.MobileApp.Commands.CommonCommands;
using PUM.MobileApp.Commands.UserReservationsCommands;
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

namespace PUM.MobileApp.ViewModels
{
    /// <summary>
    /// ViewModel class for UserReservationsView
    /// </summary>
    public class UserReservationsViewModel : ViewModelBase, IBackableViewModel, IFilterableViewModel, IAppBarableViewModel, IRefreshableViewModel, IAddableViewModel
    {
        public UserReservationsViewModel(IUserService userService)
        {
            UserService = userService;
            DownloadMyReservations();
            ShowAllUsersReservations();
        }

        #region Properties
        public IUserService UserService { get; private set; }

        private ICollection<Reservation> userReservations;

        private ObservableCollection<Reservation> userReservationsObservableCollection;
        public ObservableCollection<Reservation> UsersObservableReservationCollection
        {
            get
            {
                return userReservationsObservableCollection;
            }
            set
            {
                userReservationsObservableCollection = value;
                RaisePropertyChanged("UsersObservableReservationCollection");
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
        #endregion

        #region Commands
        #region Interfaces implementations
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

        private ICommand showAllUsersReservationsCommand;
        public ICommand ShowAllUsersReservationsCommand
        {
            get
            {
                if (showAllUsersReservationsCommand == null)
                    showAllUsersReservationsCommand = new ShowAllUsersReservationsCommand(this);

                return showAllUsersReservationsCommand;
            }
        }

        private ICommand showPastUsersReservationsCommand;
        public ICommand ShowPastUsersReservationsCommand
        {
            get
            {
                if (showPastUsersReservationsCommand == null)
                    showPastUsersReservationsCommand = new ShowPastUsersReservationsCommand(this);

                return showPastUsersReservationsCommand;
            }
        }

        private ICommand showUpcomingUsersReservationsCommand;
        public ICommand ShowUpcomingUsersReservationsCommand
        {
            get
            {
                if (showUpcomingUsersReservationsCommand == null)
                    showUpcomingUsersReservationsCommand = new ShowUpcomingUsersReservationsCommand(this);

                return showUpcomingUsersReservationsCommand;
            }
        }
        #endregion
        #endregion

        #region Methods
        #region Interfaces implementations
        public void FilterCollection(object parameter = null)
        {
            IsWorking = true;

            var filterValue = (String)parameter;

            if (filterValue == "Paid")
            {
                UsersObservableReservationCollection = new ObservableCollection<Reservation>(UsersObservableReservationCollection.Where(x => x.Fee == true));
            }
            else if (filterValue == "NotPaid")
            {
                UsersObservableReservationCollection = new ObservableCollection<Reservation>(UsersObservableReservationCollection.Where(x => x.Fee == false));
            }
            else
            {
                UsersObservableReservationCollection = new ObservableCollection<Reservation>(UsersObservableReservationCollection);
            }

            IsWorking = false;
        }

        public void RefreshView()
        {
            DownloadMyReservations();
            switch (currentView)
            {
                case "Past reservations":
                    ShowPastUsersReservations(); break;
                case "Upcoming reservations":
                    ShowUpcomingUsersReservations(); break;
                default:
                    ShowAllUsersReservations(); break;
            }
        }

        public void AddItem()
        {
            throw new System.NotImplementedException();
        }
        #endregion
        public async Task ShowAllUsersReservations()
        {
            IsWorking = true;

            UsersObservableReservationCollection = new ObservableCollection<Reservation>(userReservations);

            CurrentView = "All reservations";

            IsWorking = false;
        }

        public async Task ShowPastUsersReservations()
        {
            IsWorking = true;

            UsersObservableReservationCollection = new ObservableCollection<Reservation>(userReservations.Where(x => x.Date < DateTime.Now));

            CurrentView = "Past reservations";

            IsWorking = false;
        }

        public async Task ShowUpcomingUsersReservations()
        {
            IsWorking = true;

            UsersObservableReservationCollection = new ObservableCollection<Reservation>(userReservations.Where(x => x.Date > DateTime.Now));

            CurrentView = "Upcoming reservations";

            IsWorking = false;
        }

        private async Task DownloadMyReservations()
        {
            var uriString = "http://localhost/api/reservations/getuserreservations?";
            uriString += "userID=" + UserService.CurrentUser.UserID.ToString();

            var uri = new Uri(uriString);
            var client = new HttpClient();
            var response = await client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();
            userReservations = JsonConvert.DeserializeObject<List<Reservation>>(content);
        }
        #endregion
    }
}
