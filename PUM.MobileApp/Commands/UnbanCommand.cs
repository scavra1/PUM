namespace PUM.MobileApp.Commands
{
    using PUM.MobileApp.ViewModels;
    using PUM.SharedModels;
    using System;
    using System.Net.Http;
    using System.Windows.Input;
    using Windows.UI.Popups;

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

        public async void Execute(object parameter)
        {
            if (parameter is Ban)
            {
                var baseUri = @"http://localhost/api/bans/removeBan?id=";

                baseUri += (parameter as Ban).BanID;

                var client = new HttpClient();

                var response = await client.DeleteAsync(baseUri);
                var responseContent = await response.Content.ReadAsStringAsync();

                var dialog = new MessageDialog(responseContent);

                var command = await dialog.ShowAsync();
            }

            viewModel.RefreshView();
        }

        private BansViewModel viewModel;
    }
}
