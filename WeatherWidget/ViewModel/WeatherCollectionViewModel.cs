using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherWidget.APIConnect;

namespace WeatherWidget.ViewModel
{
    public class WeatherCollectionViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<WeatherViewModel> _weatherItems;
        private WeatherRequester _requester;
        public Thread _updateThread;
        #endregion

        #region Properties

        public ObservableCollection<WeatherViewModel> WeatherItems
        {
            get { return _weatherItems; }
        }

        public WeatherRequester Requester
        {
            get { return _requester; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an empty weather collection view model
        /// </summary>
        public WeatherCollectionViewModel()
        {
            _weatherItems = new ObservableCollection<WeatherViewModel>();
            _requester = new WeatherRequester();
            _weatherItems.CollectionChanged += _requester.WeatherItemsOnCollectionChanged;
            
        }

        public WeatherCollectionViewModel(WeatherSettings settings)
        {
            _weatherItems = new ObservableCollection<WeatherViewModel>();
            _requester = new WeatherRequester();
            _weatherItems.CollectionChanged +=_requester.WeatherItemsOnCollectionChanged;
            if (settings.WeatherItems.Any())
            {
                foreach (var weatherViewModel in settings.WeatherItems)
                {
                    _weatherItems.Add(weatherViewModel);
                }
            }
        }



        #endregion

        #region Methods
        /// <summary>
        /// Adds a new weather view model to the list
        /// </summary>
        /// <param name="model"></param>
        public void AddNewWeatherItem(WeatherViewModel model)
        {
            WeatherItems.Add(model);
        }


        /// <summary>
        /// Starts requesting updates
        /// </summary>
        public void StartUpdating()
        {
            _updateThread = new Thread(_requester.Start);
            _updateThread.Name = "WeatherUpdaterThread";
            _updateThread.IsBackground = true;
            _updateThread.Start();
        }

        /// <summary>
        /// Stops requesting updates
        /// </summary>
        public void StopUpdating()
        {
            _requester.Stop();
        }


        #endregion

        #region Events

        #endregion
    }
}
