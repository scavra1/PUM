namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;
    using PUM.MobileApp.Commands;
    using PUM.MobileApp.Services;
    using System.ComponentModel;
    using System.Windows.Input;

    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(INavigationService navigationService, IUserService userService)
        {
            this.UserService = userService;
            this.NavigationService = navigationService;
#if DEBUG
            this.Login = "handy";
            this.Password = "password";
#endif
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

        public IUserService UserService { get; private set; }

        private string password;

        private string login;

        private bool isWorking;

        private ICommand loginCommand;

        public INavigationService NavigationService;
    }
}
