using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherWidget.Annotations;

namespace WeatherWidget.Model
{
    public class Location : INotifyPropertyChanged
    {
        #region Fields

        private bool _isChecked;

        #endregion

        #region Properties

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; OnPropertyChanged(); }
        }

        public string CityName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Country { get; set; }

        #endregion

        #region Constructor

        public Location(string cityName, string lat, string longitude, string country = null)
        {
            CityName = cityName;
            Latitude = lat;
            Longitude = longitude;
            Country = ", "+country;
        }

        #endregion

        #region Methods

        #endregion

        #region Events

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
