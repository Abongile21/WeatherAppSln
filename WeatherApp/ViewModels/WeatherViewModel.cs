using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private readonly WeatherAPIService _weatherService = new WeatherAPIService();

        private WeatherInformation _weather;
        public WeatherInformation Weather
        {
            get => _weather;
            set { _weather = value; OnPropertyChanged(); }
        }

        private string _city;
        public string City
        {
            get => _city;
            set { _city = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }
        public ICommand CurrentLocationCommand { get; }

        public WeatherViewModel()
        {
            SearchCommand = new Command(async () => await LoadWeatherByCityAsync(City));
            LoadWeatherByCurrentLocationAsync();
        }

        public async Task LoadWeatherByCityAsync(string cityName)
        {
            if (!string.IsNullOrWhiteSpace(cityName))
                Weather = await _weatherService.GetWeatherByCityAsync(cityName);
        }

        public async Task LoadWeatherByCurrentLocationAsync()
        {
            try
            {
                var location = await Geolocation.Default.GetLastKnownLocationAsync();
                if (location == null)
                    location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));

                if (location != null)
                {
                    Weather = await _weatherService.GetWeatherByCoordinatesAsync(location.Latitude, location.Longitude);
                    City = Weather.name;
                }
            }
            catch
            {
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
