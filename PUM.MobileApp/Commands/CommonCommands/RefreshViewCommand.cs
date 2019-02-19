using PUM.MobileApp.ViewModels.Interfaces;
using System;
using System.Windows.Input;

namespace PUM.MobileApp.Commands.CommonCommands
{
    public class RefreshViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IRefreshableViewModel viewModel;

        public RefreshViewCommand(IRefreshableViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.RefreshView();
        }
    }
}
