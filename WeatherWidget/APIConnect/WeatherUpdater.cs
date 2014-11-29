using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WeatherWidget
{
    /// <summary>
    /// Encapsulates weather updates
    /// </summary>
    public class WeatherUpdater 
    {
        #region Fields

        #endregion

        #region Properties
        public int CurrentTemp { get; private set; }
        public string FeelsLikeTemp { get; private set; }
        public string CurrentWeatherDescription { get; private set; }
        public string WindSpeedMph { get; private set; }
        public string WindDirection { get; private set; }
        #endregion

        #region Constructor
        
        #endregion

        #region Methods
        /// <summary>
        /// Parse downloaded xml data for new weather information
        /// </summary>
        /// <param name="doc"></param>
        public void ParseWeatherXml(XmlDocument doc)
        {
            var nodeCollection = doc.SelectNodes("/data/current_condition");
            if (nodeCollection != null && nodeCollection.Count > 0)
            {
                foreach (XmlNode node in nodeCollection)
                {
                    string tempF = node["temp_F"].InnerText;
                    string windspeed = node["windspeedMiles"].InnerText;
                    string windDirectionDegree = node["winddirDegree"].InnerText;
                    string windDirectionCompass = node["winddir16Point"].InnerText;
                    CurrentTemp = Int32.Parse(tempF);
                    CurrentWeatherDescription = node["weatherDesc"].InnerText;
                    WindSpeedMph = windspeed + " MPH";
                    WindDirection = windDirectionDegree+"° "+windDirectionCompass;
                    FeelsLikeTemp = "Feels Like "+node["FeelsLikeF"].InnerText + " °F";
                }
            }
        }
        #endregion

        #region Events

        #endregion

    }
}
