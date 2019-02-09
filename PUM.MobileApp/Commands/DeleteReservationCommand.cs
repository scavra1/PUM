namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.ViewModels;
    using PUM.SharedModels;
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public class DeleteReservationCommand : ICommand
    {
        public DeleteReservationCommand(ReservationsViewModel viewModel)
        {
            this.viewModel = viewModel;
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
                return reservation.UserID.HasValue;
            }
        }

        public void Execute(object parameter)
        {
            Debug.WriteLine("gfidojbiobdjo");
            //throw new NotImplementedException();
        }

        private ReservationsViewModel viewModel;
    }
}
