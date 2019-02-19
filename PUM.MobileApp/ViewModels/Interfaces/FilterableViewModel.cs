using PUM.MobileApp.Commands.CommonCommands;
using System.Windows.Input;

namespace PUM.MobileApp.ViewModels.Interfaces
{
    public interface IFilterableViewModel
    {
        ICommand ApplyFilterCommand { get; }

        void FilterCollection(object parameter);
    }

    public class FilterableViewModel : IFilterableViewModel
    {
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

        public void FilterCollection(object parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
