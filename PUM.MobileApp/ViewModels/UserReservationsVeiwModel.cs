using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using PUM.MobileApp.Commands;
using PUM.MobileApp.Services;
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
    public class UserReservationsViewModel : ViewModelBase, IBackableViewModel
    {
        public UserReservationsViewModel(IUserService userService)
        {
            UserService = userService;
            ShowAllUsersReservations();
        }

        #region Properties
        public IUserService UserService { get; private set; }

        private ObservableCollection<Reservation> userReservationsCollection;
        public ObservableCollection<Reservation> UsersReservationCollection
        {
            get
            {
                return userReservationsCollection;
            }
            set
            {
                userReservationsCollection = value;
                RaisePropertyChanged("UsersReservationCollection");
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

        private String currentView;
        public String CurrentView
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

        #region Commands
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

        public async Task ShowAllUsersReservations()
        {
            IsWorking = true;

            UsersReservationCollection = new ObservableCollection<Reservation>(await DownloadMyReservations());

            CurrentView = "All reservations";

            IsWorking = false;
        }

        public async Task ShowPastUsersReservations()
        {
            IsWorking = true;

            var allReservations = await DownloadMyReservations();

            UsersReservationCollection = new ObservableCollection<Reservation>(allReservations.Where(x => x.Date < DateTime.Now));

            CurrentView = "Past reservations";

            IsWorking = false;
        }

        public async Task ShowUpcomingUsersReservations()
        {
            IsWorking = true;

            var allReservations = await DownloadMyReservations();

            UsersReservationCollection = new ObservableCollection<Reservation>(allReservations.Where(x => x.Date > DateTime.Now));

            CurrentView = "Upcoming reservations";

            IsWorking = false;
        }

        private async Task<ICollection<Reservation>> DownloadMyReservations()
        {
            var uriString = "http://localhost/api/reservations/getuserreservations?";
            uriString += "userID=" + UserService.CurrentUser.UserID.ToString();

            var uri = new Uri(uriString);
            var client = new HttpClient();
            var response = await client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(content);

            return new ObservableCollection<Reservation>(reservations);
        }
    }
}
