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
                    {"Moderate or heavy snow in area with thunder", @"../Images/HeavySnow.png"},
                    {"Patchy light snow in area with thunder", @"../Images/LightSnow.png"},
                    {"Moderate or heavy rain in area with thunder", @"../Images/Thunderstorms.png"},
                    {"Patchy light rain in area with thunder", @"../Images/LightRain.png"},
                    {"Moderate or heavy showers of ice pellets", @"../Images/na.png"},
                    {"Light showers of ice pellets", @"../Images/na.png"},
                    {"Moderate or heavy snow showers", @"../Images/HeavySnow.png"},
                    {"Light snow showers", @"../Images/LightSnow.png"},
                    {"Moderate or heavy sleet showers", @"../Images/Sleet.png"},
                    {"Light sleet showers", @"../Images/Sleet.png"},
                    {"Torrential rain shower", @"../Images/HeavyRain.png"},
                    {"Moderate or heavy rain shower", @"../Images/HeavyRain.png"},
                    {"Light rain shower", @"../Images/LightRain.png"},
                    {"Ice pellets", @"../Images/Hail.png"},
                    {"Heavy snow", @"../Images/HeavySnow.png"},
                    {"Patchy heavy snow", @"../Images/HeavySnow.png"},
                    {"Moderate snow", @"../Images/Snow.png"},
                    {"Patchy moderate snow", @"../Images/Snow.png"},
                    {"Light snow", @"../Images/LightSnow.png"},
                    {"Patchy light snow", @"../Images/LightSnow.png"},
                    {"Moderate or heavy sleet", @"../Images/HeavySleet.png"},
                    {"Light sleet", @"../Images/Sleet.png"},
                    {"Moderate or Heavy freezing rain", @"../Images/HeavySleet.png"},
                    {"Light freezing rain", @"../Images/Sleet.png"},
                    {"Heavy rain", @"../Images/HeavyRain.png"},
                    {"Heavy rain at times", @"../Images/HeavyRain.png"},
                    {"Moderate rain", @"../Images/Rain.png"},
                    {"Moderate rain at times", @"../Images/Rain.png"},
                    {"Light rain", @"../Images/LightRain.png"},
                    {"Patchy light rain", @"../Images/LightRain.png"},
                    {"Heavy freezing drizzle", @"../Images/HeavySleet.png"},
                    {"Freezing drizzle", @"../Images/Sleet.png"},
                    {"Light drizzle", @"../Images/LightRain.png"},
                    {"Patchy light drizzle", @"../Images/LightRain.png"},
                    {"Freezing fog", @"../Images/Fog.png"},
                    {"Fog", @"../Images/Fog.png"},
                    {"Blizzard", @"../Images/Blizzard.png"},
                    {"Blowing snow", @"../Images/HeavySnow.png"},
                    {"Thundery outbreaks in nearby", @"../Images/Thunderstorms.png"},
                    {"Patchy freezing drizzle nearby", @"../Images/Sleet.png"},
                    {"Patchy sleet nearby", @"../Images/Sleet.png"},
                    {"Patchy snow nearby", @"../Images/LightSnow.png"},
                    {"Patchy rain nearby", @"../Images/LightRain.png"},
                    {"Mist", @"../Images/Haze.png"},
                    {"Overcast", @"../Images/na.png"},
                    {"Cloudy", @"../Images/Cloudy.png"},
                    {"Partly Cloudy", @"../Images/PartlyCloud.png"},
                    {"Clear", @"../Images/ClearPM.png"},
                    {"Sunny",@"../Images/ClearAM.png"}
                };
            }

            #endregion
        }
    }
}
