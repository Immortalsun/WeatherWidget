using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WeatherWidget.Model;

namespace WeatherWidget.ViewModel
{
    /// <summary>
    /// Encapsulates properties of weather information such as Location, Latitude, Longitude, and current weather
    /// </summary>
    public class WeatherViewModel : ViewModelBase
    {
        #region Fields
        private string _currentTemp;
        private string _feelsLikeTemp;
        private string _currentDesc;
        private string _windSpeedMph;
        private string _windDir;
        private string _imagePath;
        #endregion

        #region Properties
        public string CityName { get; private set; } //required
        public string Latitude { get; private set; } //optional
        public string Longitude { get; private set; }//optional
        public string Country { get; private set; }

        public WeatherUpdater Updater { get; private set; }

  

        public string CurrentTemp
        {
            get { return _currentTemp; }
            set
            {
                _currentTemp = value;
                OnPropertyChanged();
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

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged(); }
        }

        #endregion

        #region Constructor

        public WeatherViewModel(Location loc)
        {
            CityName = loc.CityName;
            Latitude = loc.Latitude;
            Longitude = loc.Longitude;
            Country = loc.Country;
            Updater = new WeatherUpdater(CityName);
            Updater.WeatherUpdateEvent +=WeatherUpdated;
        }

        #endregion

        #region Methods
        

        #endregion

        #region Events

        public void WeatherUpdated(object sender, NotifyWeatherUpdatedEventArgs e)
        {
            CurrentTemp = e.CurrentTemp;
            CurrentWeatherDescription = e.CurrentWeatherDescription;
            WindSpeedMph = e.WindSpeedMph;
            WindDirection = e.WindDirection;
            FeelsLikeTemp = e.FeelsLikeTemp;
            ImagePath = e.ImagePath;
        }

        #endregion
    }
}
