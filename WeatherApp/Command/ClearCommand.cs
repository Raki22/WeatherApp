using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Model;
using WeatherApp.ViewModel;

namespace WeatherApp.Command
{
    public class ClearCommand : ICommand
    {
        public MainViewModel MainViewModel { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ClearCommand(MainViewModel vm)
        {
            MainViewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            string query = parameter as string;

            if (string.IsNullOrWhiteSpace(query))
                return false;
            
            return true;
        }

        public void Execute(object parameter)
        {
            MainViewModel.QueryTextBox = string.Empty;
            MainViewModel.Cities.Clear();
        }
    }
}
