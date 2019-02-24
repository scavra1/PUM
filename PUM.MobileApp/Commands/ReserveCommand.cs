namespace PUM.MobileApp.Commands
{
    using CommonServiceLocator;
    using Newtonsoft.Json;
    using PUM.MobileApp.Services;
    using PUM.MobileApp.ViewModels;
    using PUM.SharedModels;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Windows.Input;
    using Windows.UI.Popups;

    public class ReserveCommand : ICommand
    {
        public ReserveCommand(ReservationsViewModel viewModel, IUserService userService)
        {
            this.viewModel = viewModel;
            this.userService = userService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var reservation = (Reservation)parameter;

            if (reservation != null)
            {
                return !(reservation.UserID.HasValue);
            }
            else
            {
                return true;
            }

        }

        public async void Execute(object parameter)
        {
            var reservation = (Reservation)parameter;

            if (reservation != null)
            {

                var currentUser = userService.CurrentUser;
                var reservationModel = new Reservation
                {
                    UserID = currentUser.UserID,
                    Date = reservation.Date,
                    DateKey = reservation.DateKey,
                    HourKey = reservation.HourKey
                };

                var model = JsonConvert.SerializeObject(reservationModel);
                var requestContent = new StringContent(model, Encoding.UTF8, "application/json");

                var baseUri = @"http://localhost/api/reservations/addreservation";
                var client = new HttpClient();

                var response = await client.PostAsync(baseUri, requestContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                var dialog = new MessageDialog(responseContent);

                var command = await dialog.ShowAsync();

                viewModel.DownloadReservations();
            }
        }

        private ReservationsViewModel viewModel;
        private IUserService userService;
    }
}
