using PUM.MobileApp.Commands.CommonCommands;
using System.Windows.Input;

namespace PUM.MobileApp.ViewModels.Interfaces
{
    public interface IFilterableViewModel
    {
        ICommand ApplyFilterCommand { get; }

        string LastFilter { get; set; }

        void FilterCollection(object parameter = null);
    }

    public class FilterableViewModel : IFilterableViewModel
    {
        public string LastFilter { get; set; }

        private ICommand applyFilterCommand;
        public ICommand ApplyFilterCommand
        {
            get
            {
                if (applyFilterCommand == null)
                    applyFilterCommand = new ApplyFilterCommand(this);

                return applyFilterCommand;
            }
        }

        public void FilterCollection(object parameter = null)
        {
            if (parameter != null)
            {
                LastFilter = (string)parameter;
            }

            throw new System.NotImplementedException();
        }
    }
}
