namespace PUM.MobileApp.ViewModels
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;
    using PUM.MobileApp.Services;
    using PUM.MobileApp.Views;

    /// <summary>
    /// Class that allows to navigate between views
    /// </summary>
    public class ViewModelLocator
    { 
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();

            nav.Configure("Bans", typeof(BansView));
            nav.Configure("Fees", typeof(FeesView));
            nav.Configure("Login", typeof(LoginView));
            nav.Configure("MainMenu", typeof(MainMenuView));
            nav.Configure("Reservations", typeof(ReservationsView));
            nav.Configure("UserReservations", typeof(UserReservationsView));

            SimpleIoc.Default.Register<IUserService, UserService>();
            SimpleIoc.Default.Register<INavigationService>(() => nav);

            SimpleIoc.Default.Register<BansViewModel>();
            SimpleIoc.Default.Register<FeesViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<ReservationsViewModel>();
            SimpleIoc.Default.Register<MainMenuViewModel>();
            SimpleIoc.Default.Register<UserReservationsViewModel>();
        }

        public BansViewModel BansViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BansViewModel>();
            }
        }

        public LoginViewModel LoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public MainMenuViewModel MainMenuViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainMenuViewModel>();
            }
        }

        public FeesViewModel FeesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FeesViewModel>();
            } 
        }

        public ReservationsViewModel ReservationsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ReservationsViewModel>();
            }
        }

        public UserReservationsViewModel UserReservationsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserReservationsViewModel>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}
