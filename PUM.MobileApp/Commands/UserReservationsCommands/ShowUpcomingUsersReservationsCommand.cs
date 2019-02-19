using PUM.MobileApp.ViewModels;
using System;
using System.Windows.Input;

namespace PUM.MobileApp.Commands.UserReservationsCommands
{
    public class ShowUpcomingUsersReservationsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private UserReservationsViewModel viewModel;

        public ShowUpcomingUsersReservationsCommand(UserReservationsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.ShowUpcomingUsersReservations();
        }
    }
}
