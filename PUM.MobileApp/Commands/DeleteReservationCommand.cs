namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.Services;
    using PUM.MobileApp.ViewModels;
    using PUM.SharedModels;
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Windows.Input;
    using Windows.UI.Popups;

    public class DeleteReservationCommand : ICommand
    {
        public DeleteReservationCommand(ReservationsViewModel viewModel, IUserService userService)
        {
            this.viewModel = viewModel;
            this.userService = userService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var reservation = (Reservation)parameter;

            if (reservation == null)
            {
                return false;
            }
            else
            {
                return reservation.UserID.HasValue && reservation.UserID.Value == userService.CurrentUser.UserID;
            }
        }

        public async void Execute(object parameter)
        {
            var reservation = (Reservation)parameter;

            if (reservation != null)
            {
                var client = new HttpClient();
                var requestUri = $@"http://localhost/api/reservations/deletereservation?reservationid={reservation.ReservationID.Value}";

                var response = await client.DeleteAsync(requestUri);

                var dialog = new MessageDialog("Rezerwacja została usunięta.");

                var command = await dialog.ShowAsync();

                viewModel.DownloadReservations();
            }
        }

        private ReservationsViewModel viewModel;
        private IUserService userService;
    }
}
