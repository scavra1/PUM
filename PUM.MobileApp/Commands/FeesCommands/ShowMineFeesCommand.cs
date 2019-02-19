using PUM.MobileApp.ViewModels;
using System;
using System.Windows.Input;

namespace PUM.MobileApp.Commands.FeesCommands
{
    public class ShowMineFeesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private FeesViewModel viewModel;

        public ShowMineFeesCommand(FeesViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.ShowMineFees();
        }
    }
}
