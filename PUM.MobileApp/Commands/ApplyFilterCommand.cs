using PUM.MobileApp.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PUM.MobileApp.Commands
{
    public class ApplyFilterCommand :ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IFilterableViewModel viewModel;

        public ApplyFilterCommand(IFilterableViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.FilterCollection(parameter);
        }
    }
}
