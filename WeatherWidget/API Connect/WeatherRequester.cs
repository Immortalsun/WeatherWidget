using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WeatherWidget.Model;

namespace WeatherWidget
{
    /// <summary>
    /// Class that handles requestin the online weather API for information
    /// </summary>
    public class WeatherRequester
    {

        #region Fields

        private const string WeatherRequestURL = "http://api.worldweatheronline.com/free/v2/weather.ashx?";
        private const string WeatherSearchURL = "http://api.worldweatheronline.com/free/v2/search.ashx?";

        private List<WeatherViewModel> WeatherList;
        private bool _updating;
        private BackgroundWorker UpdateWeather;
        #endregion

        #region Properties
        
        #endregion

        #region Constructor

        public WeatherRequester()
        {
            WeatherList = new List<WeatherViewModel>();
            UpdateWeather = new BackgroundWorker();
            UpdateWeather.DoWork += Update;
        }


        #endregion

        #region Methods
        /// <summary>
        /// Method to update the weather objects
        /// </summary>
        public void Update(object sender, DoWorkEventArgs e)
        {
            _updating = true;
            while (_updating)
            {
                if (WeatherList.Any())
                {
                    foreach (var model in WeatherList)
                    {

                    }
                }
            }

        }

        /// <summary>
        /// Loads a list of weather view model objects
        /// from a serialized xml file
        /// </summary>
        public void Load()
        {
        }

        /// <summary>
        /// Saves the list of current weather viwe model objects
        /// to a serialized xml file
        /// </summary>
        public void Save()
        {
        }

        public void GenerateRequestString(string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(WeatherRequestURL);
            sb.Append("key=");
            sb.Append(ApiKey.Key);
        }

        #endregion

        #region Events

        #endregion

    }
}
