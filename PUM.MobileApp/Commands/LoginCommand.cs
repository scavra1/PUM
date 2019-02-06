namespace PUM.MobileApp.Commands
{
    using Newtonsoft.Json;
    using PUM.MobileApp.ViewModels;
    using PUM.SharedModels;
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class LoginCommand : ICommand
    {

        public LoginCommand(LoginViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, null);
        }

        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(viewModel.Login)
                    && !string.IsNullOrWhiteSpace(viewModel.Password);
        }

        public async void Execute(object parameter)
        {
            var client = new HttpClient();

            viewModel.IsWorking = true;

            var credentials = JsonConvert.SerializeObject(new UserCredentials { Username = viewModel.Login, Password = viewModel.Password });
            var requestBody = new StringContent(credentials, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost/api/users/finduser", requestBody);
            var content = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<User>(content);

            if (user != null)
            {
                Debug.Print("User downloaded:" + user.FirstName + " " + user.LastName);
            }
            else
            {
                Debug.Print("DUPAAAA");
            }

            await Task.Delay(5000);

            viewModel.IsWorking = false;

            viewModel.NavigationService.NavigateTo("MainMenu");
        }

        private LoginViewModel viewModel;
    }
}
