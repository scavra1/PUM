namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using PUM.MobileApp.Commands;
    using System.Collections.ObjectModel;
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
                if (getFeesCommand == null)
                    getFeesCommand = new GetFeesCommand(this);

                return GetFeesCommand;
            }
        }

        private ICommand getFeesCommand;
    }
}
