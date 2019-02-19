using PUM.MobileApp.ViewModels;
using System;
using System.Windows.Input;

namespace PUM.MobileApp.Commands.BansCommands
{
    public class ShowPastBansCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private BansViewModel viewModel;

        public ShowPastBansCommand(BansViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.ShowPastBans();
        }
    }
}
