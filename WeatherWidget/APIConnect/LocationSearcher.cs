using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WeatherWidget.Model;

namespace WeatherWidget.APIConnect
{
    public class LocationSearcher
    {
        #region Fields

        #endregion

        #region Properties
        public string QueryString { get; set; }
        #endregion

        #region Constructor

        #endregion

        #region Methods
        public void GenerateSearchQuery(string locationName)
        {
            locationName = locationName.Trim();
            locationName = locationName.Replace(' ', '_');

            StringBuilder sb = new StringBuilder();
            sb.Append(WeatherRequester.WeatherSearchURL);
            sb.Append("key=");
            sb.Append(ApiKey.Key);
            sb.Append("&q=");
            sb.Append(locationName);
            //sb.Append("&popular=yes"); //narrow results to only show popular cities
            sb.Append("&format=xml");

            QueryString = sb.ToString();
        }

        public List<Location> ParseLocationXml(XmlDocument xml)
        {
            var locationList = new List<Location>();

            var nodeCollection = xml.SelectNodes("/search_api/result");
            if (nodeCollection != null && nodeCollection.Count > 0)
            {
                foreach (XmlNode node in nodeCollection)
                {
                    var locationName = node["areaName"].InnerText;
                    var country = node["country"].InnerText;
                    var latitude = node["latitude"].InnerText;
                    var longitude = node["longitude"].InnerText;
                    if (country.Equals("United States of America"))
                    {
                        country = node["region"].InnerText;
                    }
                    locationList.Add(new Location(locationName, latitude, longitude, country));
                }
            }

            return locationList;
        }

        #endregion

        #region Events

        #endregion
    }
}
