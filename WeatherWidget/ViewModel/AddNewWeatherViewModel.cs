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

        private RelayCommand _okCommand;
        private RelayCommand _cancelCommand;
        private MainWindowViewModel _parent;
        #endregion

        #region Properties

        public RelayCommand OKCommand
        {
            get { return _okCommand; }
        }

        public RelayCommand CancelCommand
        {
            get { return _cancelCommand; }
        }

        public String CityName { get; set; }

        #endregion

        #region Constructor

        public AddNewWeatherViewModel(RelayCommand okCommand, RelayCommand cancelCommand)
        {
            _okCommand = okCommand;
            _cancelCommand = cancelCommand;
        }

        #endregion

        #region Methods
        
        #endregion

        #region Events

        #endregion
    }
}
