namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.CustomControls;
    using PUM.MobileApp.ViewModels;
    using PUM.SharedModels;
    using System;
    using System.Windows.Input;

    public class EditBanCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public BansViewModel viewModel;

        public EditBanCommand(BansViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

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

                switch (viewModel.CurrentView)
                {
                    case "Past bans":
                        viewModel.ShowPastBans(); break;
                    case "Active bans":
                        viewModel.ShowActiveBans(); break;
                    default:
                        viewModel.ShowAllBans(); break;
                }
            }
        }
    }
}
