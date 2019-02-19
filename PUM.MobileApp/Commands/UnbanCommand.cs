namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.ViewModels;
    using PUM.SharedModels;
    using System;
    using System.Windows.Input;

    public class UnbanCommand : ICommand
    {
        public UnbanCommand(BansViewModel viewModel)
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
            if (parameter is Ban)
            {
                
            }
        }

        private BansViewModel viewModel;
    }
}
