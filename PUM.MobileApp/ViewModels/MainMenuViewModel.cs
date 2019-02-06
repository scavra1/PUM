namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;
    using PUM.MobileApp.Commands;
    using System.Windows.Input;

    public class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                    logoutCommand = new LogoutCommand(this);
                return logoutCommand;
            }
        }

        public ICommand NavigateBansCommand
        {
            get
            {
                if (navigateBansCommand == null)
                    navigateBansCommand = new NavigateBansCommand(this);

                return navigateBansCommand;
            }
        }

        public ICommand NavigateFeesCommand
        {
            get
            {
                if (navigateFeesCommand == null)
                    navigateFeesCommand = new NavigateFeesCommand(this);

                return navigateFeesCommand;
            }
        }

        public ICommand NavigateReservationsCommand
        {
            get
            {
                if (navigateReservationsCommand == null)
                    navigateReservationsCommand = new NavigateReservationsCommand(this);

                return navigateReservationsCommand;
            }
        }

        private ICommand navigateBansCommand;
        private ICommand navigateFeesCommand;
        private ICommand navigateReservationsCommand;

        private ICommand logoutCommand;

        public INavigationService NavigationService;
    }
}
