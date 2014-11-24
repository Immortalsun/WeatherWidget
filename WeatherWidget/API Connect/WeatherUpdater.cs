using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
                    CurrentTemp = Int32.Parse(tempF);
                }
            }
        }
        #endregion

        #region Events

        #endregion

    }
}
