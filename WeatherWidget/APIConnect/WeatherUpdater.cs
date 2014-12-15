using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WeatherWidget.Annotations;
using WeatherWidget.APIConnect;

namespace WeatherWidget
{
    /// <summary>
    /// Encapsulates weather updates
    /// </summary>
    public class WeatherUpdater : INotifyWeatherUpdate
    {
        #region Fields

        #endregion

        #region Properties
        public string QueryString { get; private set; }
        #endregion

        #region Constructor

        public WeatherUpdater(string cityName)
        {
            QueryString = GenerateRequestString(cityName);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parse downloaded xml data for new weather information
        /// </summary>
        /// <param name="doc"></param>
        public void Update(XmlDocument doc)
        {
            var nodeCollection = doc.SelectNodes("/data/current_condition");
            if (nodeCollection != null && nodeCollection.Count > 0)
            {
                var updaterArgs = new NotifyWeatherUpdatedEventArgs();
                foreach (XmlNode node in nodeCollection)
                {

                    string windspeed = node["windspeedMiles"].InnerText;
                    string windDirectionDegree = node["winddirDegree"].InnerText;
                    string windDirectionCompass = node["winddir16Point"].InnerText;
                    updaterArgs.CurrentTemp = node["temp_F"].InnerText + " °F";
                    updaterArgs.CurrentWeatherDescription = node["weatherDesc"].InnerText;
                    updaterArgs.WindSpeedMph = windspeed + " MPH";
                    updaterArgs.WindDirection = windDirectionDegree + "° " + windDirectionCompass;
                    updaterArgs.FeelsLikeTemp = "Feels Like " + node["FeelsLikeF"].InnerText + " °F";
                    updaterArgs.ImagePath = node["weatherIconUrl"].InnerText;
                }
                WeatherUpdateEvent.Invoke(this, updaterArgs);
            }
        }

        /// <summary>
        /// Generates a request call to the api 
        /// for the specified city name
        /// </summary>
        /// <param name="name">Name of city we are requesting</param>
        /// <returns></returns>
        public string GenerateRequestString(string name)
        {
            name = name.Trim();
            name = name.Replace(' ', '_');

            StringBuilder sb = new StringBuilder();
            sb.Append(WeatherRequester.WeatherRequestURL);
            sb.Append("key=");
            sb.Append(ApiKey.Key);
            sb.Append("&q=");
            sb.Append(name);
            sb.Append("&format=xml");

            return sb.ToString();
        }



        #endregion

        #region NotifyWeatherUpdated
        public event NotifyWeatherUpdatedEvent WeatherUpdateEvent;
        #endregion

    }
}
