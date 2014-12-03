using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWidget
{
    public interface INotifyWeatherUpdate
    {
        event NotifyWeatherUpdatedEvent WeatherUpdateEvent;
    }

    public delegate void NotifyWeatherUpdatedEvent(object sender, NotifyWeatherUpdatedEventArgs args);

    public class NotifyWeatherUpdatedEventArgs
    {
        public string CurrentTemp { get; set; }
        public string FeelsLikeTemp { get; set; }
        public string CurrentWeatherDescription { get; set; }
        public string WindSpeedMph { get; set; }
        public string WindDirection { get; set; }
    }
}
