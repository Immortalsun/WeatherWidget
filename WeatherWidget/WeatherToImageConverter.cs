using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherWidget
{
    public class WeatherToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is String))
            {
                return null;
            }
            return WeatherToImageMap.WeatherImageMap[value.ToString()];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static class WeatherToImageMap
        {

            #region Properties
            public static Dictionary<string, string> WeatherImageMap { get; private set; } 
            #endregion

            #region Constructor

            static WeatherToImageMap()
            {
                WeatherImageMap = new Dictionary<string, string>
                {
                    {"Moderate or heavy snow in area with thunder", ""},
                    {"Patchy light snow in area with thunder", ""},
                    {"Moderate or heavy rain in area with thunder", ""},
                    {"Patchy light rain in area with thunder", ""},
                    {"Moderate or heavy showers of ice pellets", ""},
                    {"Light showers of ice pellets", ""},
                    {"Moderate or heavy snow showers", ""},
                    {"Light snow showers", ""},
                    {"Moderate or heavy sleet showers", ""},
                    {"Light sleet showers", ""},
                    {"Torrential rain shower", ""},
                    {"Moderate or heavy rain shower", ""},
                    {"Light rain shower", ""},
                    {"Ice pellets", ""},
                    {"Heavy snow", ""},
                    {"Patchy heavy snow", ""},
                    {"Moderate snow", ""},
                    {"Patchy moderate snow", ""},
                    {"Light snow", ""},
                    {"Patchy light snow", ""},
                    {"Moderate or heavy sleet", ""},
                    {"Light sleet", ""},
                    {"Moderate or Heavy freezing rain", ""},
                    {"Light freezing rain", ""},
                    {"Heavy rain", ""},
                    {"Heavy rain at times", ""},
                    {"Moderate rain", ""},
                    {"Moderate rain at times", ""},
                    {"Light rain", ""},
                    {"Patchy light rain", ""},
                    {"Heavy freezing drizzle", ""},
                    {"Freezing drizzle", ""},
                    {"Light drizzle", ""},
                    {"Patchy light drizzle", ""},
                    {"Freezing fog", ""},
                    {"Fog", ""},
                    {"Blizzard", ""},
                    {"Blowing snow", ""},
                    {"Thundery outbreaks in nearby", ""},
                    {"Patchy freezing drizzle nearby", ""},
                    {"Patchy sleet nearby", ""},
                    {"Patchy snow nearby", ""},
                    {"Patchy rain nearby", ""},
                    {"Mist", ""},
                    {"Overcast", ""},
                    {"Cloudy", ""},
                    {"Partly Cloudy", ""},
                    {"Clear", ""},
                    {"Sunny",""}
                };
            }
        }
    }
}
