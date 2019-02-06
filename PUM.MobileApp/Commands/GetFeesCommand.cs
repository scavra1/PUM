namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.ViewModels;
    using System;
    using System.Windows.Input;

    internal class GetFeesCommand : ICommand
    {

        public GetFeesCommand(FeesViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        private FeesViewModel viewModel;
    }
}
