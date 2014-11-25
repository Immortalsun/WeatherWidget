using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWidget.ViewModel
{
    public class AddNewWeatherViewModel : ViewModelBase
    {
        #region Fields

        private string _cityNameText;
        private RelayCommand _okCommand;
        private RelayCommand _cancelCommand;

        #endregion

        #region Properties

        public RelayCommand OKCommand
        {
            get { return _okCommand ?? (_okCommand = new RelayCommand(param => OK())); }
        }

        public RelayCommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(param => Cancel())); }
        }

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public void OK()
        {

        }

        public void Cancel()
        {

        }

        #endregion

        #region Events

        #endregion
    }
}
