namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;
    using PUM.MobileApp.Commands;
    using System.ComponentModel;
    using System.Windows.Input;

    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
            this.Login = "scavra";
            this.Password = "password";
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        public bool IsWorking
        {
            get { return isWorking; }
            set
            {
                isWorking = value;
                RaisePropertyChanged("IsWorking");
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                    loginCommand = new LoginCommand(this);

                return loginCommand;
            }
        }

        private string password;

        private string login;

        private bool isWorking;

        private ICommand loginCommand;

        public INavigationService NavigationService;
    }
}
