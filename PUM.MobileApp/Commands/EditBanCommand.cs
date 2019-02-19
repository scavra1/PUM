namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.CustomControls;
    using PUM.SharedModels;
    using System;
    using System.Windows.Input;

    public class EditBanCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter is Ban)
            {
                var dialog = new EditBanDialog(parameter as Ban);

                await dialog.ShowAsync();
            }
        }
    }
}
