using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WeatherWidget.ViewModel
{
    /// <summary>
    /// Encapsulates properties of weather information such as Location, Latitude, Longitude, and current weather
    /// </summary>
    public class WeatherViewModel : ViewModelBase
    {
        #region Fields
        private int _currentTemp;
        private string _feelsLikeTemp;
        private string _currentDesc;
        private string _windSpeedMph;
        private string _windDir;
        private bool _firstLoad = true;
        #endregion

        #region Properties
        public string CityName { get; private set; } //required
        public double Latitude { get; private set; } //optional
        public double Longitude { get; private set; }//optional
        public string USState { get; private set; }
        public string Country { get; private set; }

        public WeatherUpdater Updater { get; private set; }

        public int CurrentTemp
        {
            get { return _currentTemp; }
            set
            {
                if (_currentTemp != value)
                {
                    _currentTemp = value;
                    OnPropertyChanged("CurrentTempString");
                }
            }
        }

        public string CurrentTempString
        {
            get
            {
                if (_firstLoad)
                {
                    return "Loading...";
                }
                else
                {
                    return _currentTemp + " °F";
                }
            }
        }

        public string FeelsLikeTemp
        {
            get
            {
                return _feelsLikeTemp;
            }
            set
            {
                _feelsLikeTemp = value; OnPropertyChanged();
            }
        }

        public string CurrentWeatherDescription
        {
            get { return _currentDesc; }
            set { _currentDesc = value; OnPropertyChanged(); }
        }

        public string WindSpeedMph
        {
            get { return _windSpeedMph; }
            set { _windSpeedMph = value; OnPropertyChanged(); }
        }

        public string WindDirection
        {
            get { return _windDir; }
            set { _windDir = value; OnPropertyChanged(); }
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
            Updater = new WeatherUpdater();
        }

        #endregion

        #region Methods

        public void UpdateWeather(XmlDocument doc)
        {
            _firstLoad = false;
            Updater.ParseWeatherXml(doc);
            CurrentTemp = Updater.CurrentTemp;
            WindDirection = Updater.WindDirection;
            CurrentWeatherDescription = Updater.CurrentWeatherDescription;
            WindSpeedMph = Updater.WindSpeedMph;
            FeelsLikeTemp = Updater.FeelsLikeTemp;
        }

        #endregion

        #region Events

        #endregion
    }
}
