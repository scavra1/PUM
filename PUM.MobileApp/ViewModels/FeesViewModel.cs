namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using PUM.MobileApp.Commands;
    using PUM.MobileApp.Models;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class FeesViewModel : ViewModelBase
    {
        public FeesViewModel()
        {
            FeesCollection = new ObservableCollection<Fee>();
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

        public ObservableCollection<Fee> FeesCollection { get; }

        private ICommand getFeesCommand;
    }
}
