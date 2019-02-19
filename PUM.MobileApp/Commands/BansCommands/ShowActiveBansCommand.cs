using PUM.MobileApp.ViewModels;
using System;
using System.Windows.Input;

namespace PUM.MobileApp.Commands.BansCommands
{
    public class ShowActiveBansCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private BansViewModel viewModel;

        public ShowActiveBansCommand(BansViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.ShowActiveBans();
        }
    }
}
