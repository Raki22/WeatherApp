using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Command;
using WeatherApp.Model;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<City> Cities { get; set; }
        private string _queryTextBox;
        public string QueryTextBox
        {
            get { return _queryTextBox; }
            set 
            { 
                _queryTextBox = value;
                OnPropertyChanged("QueryTextBox");
            }
        }

        private CurrentConditions _currentConditions;
        public CurrentConditions CurrentConditions
        {
            get { return _currentConditions; }
            set 
            { 
                _currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City _selectedCity;
        public City SelectedCity
        {
            get { return _selectedCity; }
            set 
            { 
                _selectedCity = value;
                OnPropertyChanged("SelectedCity");

                City emptyCity = new City();
                if (_selectedCity != null && _selectedCity != emptyCity)
                {
                    GetCurrentConditions();
                }
            }
        }

        public SearchCommand SearchCommand { get; set; }
        public ClearCommand ClearCommand { get; set; }

        public MainViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City() { LocalizedName = "El Oued" };
                CurrentConditions = new CurrentConditions()
                {
                    WeatherText = "Partly Cloudy",
                    Temperature = new Temperature() { Metric = new Measure() { Value = "21" } }
                };
            }

            SearchCommand = new SearchCommand(this);
            ClearCommand = new ClearCommand(this);
            Cities = new ObservableCollection<City>();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            // by human language this means : if PropertyChanged event does exist, then use its Invoke method...
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(QueryTextBox);
            
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        private async void GetCurrentConditions()
        {
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }
    }
}
