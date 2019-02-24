namespace PUM.MobileApp.ViewModels
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using PUM.MobileApp.Commands;
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

    public class ReservationsViewModel : ViewModelBase, IBackableViewModel, IAppBarableViewModel, IRefreshableViewModel
    {
        public ReservationsViewModel(IUserService userService)
        {
            SelectedDate = DateTime.Now;
            UserService = userService;
            CurrentView = "Make reservation";
            DownloadReservations();
        }

        #region Properties
        private DateTimeOffset selectedDate;
        public DateTimeOffset SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    RaisePropertyChanged("SelectedDate");
                }
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

        private IList<Reservation> reservations;
        public IList<Reservation> Reservations
        {
            get
            {
                return reservations;
            }
            set
            {
                reservations = value;
                RaisePropertyChanged("Reservations");
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
        #region Commands
        private ICommand reserveCommand;
        public ICommand ReserveCommand
        {
            get
            {
                if (reserveCommand == null)
                    reserveCommand = new ReserveCommand(this, UserService);

                return reserveCommand;
            }
        }

        private ICommand deleteReservationCommand;
        public ICommand DeleteReservationCommand
        {
            get
            {
                if (deleteReservationCommand == null)
                    deleteReservationCommand = new DeleteReservationCommand(this, UserService);

                return deleteReservationCommand;
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
        #endregion
        #endregion

        public IUserService UserService { get; private set; }

        public async Task DownloadReservations()
        {
            IsWorking = true;

            var baseUri = @"http://localhost/api/reservations/getreservations?dateKey=";
            var dateKey = selectedDate.ToString("yyyyMMdd");

            var methodUri = baseUri + dateKey;

            var client = new HttpClient();
            var response = await client.GetAsync(methodUri);

            var content = await response.Content.ReadAsStringAsync();
            var reservationsResponse = JsonConvert.DeserializeObject<List<Reservation>>(content);

            var dailyReservations = CreateDailyReservationEntries(reservationsResponse);

            Reservations = new ObservableCollection<Reservation>(dailyReservations);

            IsWorking = false;
        }

        private List<Reservation> CreateDailyReservationEntries(List<Reservation> reservations)
        {
            var dateKey = int.Parse(SelectedDate.ToString("yyyyMMdd"));

            var dailyReservations = new List<Reservation>();
            for (int hour = 10; hour < 21; hour++)
            {
                var reservationForHour = reservations.FirstOrDefault(r => r.HourKey == hour);

                var year = SelectedDate.Date.Year;
                var month = SelectedDate.Date.Month;
                var day = SelectedDate.Date.Day;

                var dateX = new DateTime(year, month, day, hour, 0, 0);

                if (reservationForHour == null)
                {
                    reservationForHour = new Reservation { HourKey = hour, DateKey = dateKey, Date = dateX };
                }

                reservationForHour.HourDescription = $"{hour}:00 - {hour + 1}:00";

                dailyReservations.Add(reservationForHour);
            }

            return dailyReservations;
        }

        public void RefreshView()
        {
            DownloadReservations();
        }
    }
}
