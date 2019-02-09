namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using PUM.MobileApp.Commands;
    using PUM.SharedModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class ReservationsViewModel : ViewModelBase
    {
        public ReservationsViewModel()
        {
            SelectedDate = DateTime.Now;
            DownloadReservations();
        }

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

        private ICommand reserveCommand;
        public ICommand ReserveCommand
        {
            get
            {
                if (reserveCommand == null)
                    reserveCommand = new ReserveCommand(this);

                return reserveCommand;
            }
        }

        private ICommand deleteReservationCommand;
        public ICommand DeleteReservationCommand
        {
            get
            {
                if (deleteReservationCommand == null)
                    deleteReservationCommand = new DeleteReservationCommand(this);

                return deleteReservationCommand;
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

                if (reservationForHour == null)
                {
                    reservationForHour = new Reservation { HourKey = hour, DateKey = dateKey };
                }

                reservationForHour.HourDescription = $"{hour}:00 - {hour + 1}:00";

                dailyReservations.Add(reservationForHour);
            }

            return dailyReservations;
        }
    }
}
