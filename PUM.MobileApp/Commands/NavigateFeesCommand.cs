namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.ViewModels;
    using System;
    using System.Windows.Input;

    internal class NavigateFeesCommand : ICommand
    {
        public NavigateFeesCommand(MainMenuViewModel viewModel)
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
            viewModel.NavigationService.NavigateTo("Fees");
        }

        public MainMenuViewModel viewModel;
    }
}
