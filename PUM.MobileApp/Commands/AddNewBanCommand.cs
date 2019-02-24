using PUM.MobileApp.CustomControls;
using PUM.MobileApp.ViewModels;
using System;
using System.Windows.Input;

namespace PUM.MobileApp.Commands
{
    public class AddNewBanCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private BansViewModel viewModel;

        public AddNewBanCommand(BansViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var dialog = new EditBanDialog(null);

            await dialog.ShowAsync();

            viewModel.RefreshView();
        }
    }
}
