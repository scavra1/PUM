namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.ViewModels;
    using System;
    using System.Windows.Input;

    internal class LogoutCommand : ICommand
    {

        public LogoutCommand(MainMenuViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.NavigationService.NavigateTo("Login");
        }

        private MainMenuViewModel viewModel;
    }
}
