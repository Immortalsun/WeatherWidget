using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWidget.ViewModel;

namespace WeatherWidget
{
    /// <summary>
    /// This class will act as a settings
    /// repository for the app.
    /// This is the object that will get serialized out on save
    /// </summary>
    public class WeatherSettings
    {
        #region Fields

        #endregion

        #region Properties
        public List<WeatherViewModel> WeatherItems { get; set; }
 
        #endregion

        #region Constructor

        #endregion

        #region Methods

        #endregion

        #region Events

        #endregion
    }
}
