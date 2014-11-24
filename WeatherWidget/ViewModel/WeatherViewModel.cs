using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWidget.Model
{
    /// <summary>
    /// Encapsulates properties of weather information such as Location, Latitude, Longitude, and current weather
    /// </summary>
    public class WeatherViewModel : ViewModelBase
    {
        #region Fields
        private int _currentTemp;
        #endregion

        #region Properties
        public string CityName { get; private set; } //required
        public double Latitude { get; private set; } //optional
        public double Longitude { get; private set; }//optional
        public string USState { get; private set; }
        public string Country { get; private set; }

        public int CurrentTemp
        {
            get { return _currentTemp; }
            set
            {
                if (_currentTemp != value)
                {
                    _currentTemp = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructor

        public WeatherViewModel(string cityName, double lat = 0, double lon = 0, string state = null,
            string country = null)
        {
            CityName = cityName;
            Latitude = lat;
            Longitude = lon;
            USState = state;
            Country = country;
        }

        #endregion

        #region Methods

        public void UpdateWeather()
        {

        }

        #endregion

        #region Events

        #endregion
    }
}
