namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.ViewModels;
    using System;
    using System.Windows.Input;

    internal class NavigateBansCommand : ICommand
    {
        public NavigateBansCommand(MainMenuViewModel viewModel)
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
            viewModel.NavigationService.NavigateTo("Bans");
        }

        private MainMenuViewModel viewModel;
    }
}
