using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.ViewModel;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.Command
{
    public class SearchCommand : ICommand
    {
        public MainViewModel MainViewModel { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(MainViewModel vm)
        {
            this.MainViewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            string query = parameter as string;

            if (string.IsNullOrWhiteSpace(query))
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            MainViewModel.MakeQuery();
        }
    }
}
