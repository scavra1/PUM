namespace PUM.MobileApp.Commands
{
    using CommonServiceLocator;
    using PUM.MobileApp.Services;
    using PUM.MobileApp.ViewModels;
    using PUM.SharedModels;
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public class ReserveCommand : ICommand
    {
        public ReserveCommand(ReservationsViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.userService = ServiceLocator.Current.GetInstance<UserService>();
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

        public void Execute(object parameter)
        {
            Debug.WriteLine($"vfiovjdiofvdov");
        }

        private ReservationsViewModel viewModel;
        private UserService userService;
    }
}
