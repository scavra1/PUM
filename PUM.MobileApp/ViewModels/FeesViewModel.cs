namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using System.Windows.Input;

    public class FeesViewModel : ViewModelBase
    {
        public FeesViewModel()
        {
           
        }

        public ICommand GetFeesCommand
        {
            get
            {
                return getFeesCommand;
            }
        }

        private ICommand getFeesCommand;
    }
}
