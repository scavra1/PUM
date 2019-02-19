using PUM.MobileApp.ViewModels.Interfaces;
using System;
using System.Windows.Input;

namespace PUM.MobileApp.Commands.CommonCommands
{
    public class AddItemCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IAddableViewModel viewModel;

        public AddItemCommand(IAddableViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.AddItem();
        }
    }
}
