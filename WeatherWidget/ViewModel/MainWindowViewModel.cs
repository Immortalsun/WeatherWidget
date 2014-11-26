using System.Collections.ObjectModel;
using System.Threading;

namespace WeatherWidget.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        //collection of view models
        private ObservableCollection<ViewModelBase> _viewModels;
        //add new weather view model
        private AddNewWeatherViewModel _newWeatherViewModel;
        //weather collection view model
        private WeatherCollectionViewModel _weatherCollectionViewModel;

        private ViewModelBase _currentViewModel;
        //commands to switch views
        private RelayCommand _addNewWeatherCommand;
        private RelayCommand _showWeatherViewCommand;


        #endregion

        #region Properties

        public ObservableCollection<ViewModelBase> ViewModels
        {
            get { return _viewModels; }
        }

        public RelayCommand AddNewWeatherCommand
        {
            get
            {
                return _addNewWeatherCommand ?? (_addNewWeatherCommand = new RelayCommand(param => AddNewWeather()));
            }
        }

        public RelayCommand ShowWeatherViewCommand
        {
            get
            {
                return _showWeatherViewCommand ??
                       (_showWeatherViewCommand = new RelayCommand(param => ShowWeatherView()));
            }
        }

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
                
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            _viewModels = new ObservableCollection<ViewModelBase>();
            _newWeatherViewModel = new AddNewWeatherViewModel();
            _viewModels.Add(_newWeatherViewModel);
            SetActiveViewModel(_newWeatherViewModel);
        }



        #endregion

        #region Methods

        public void ShowWeatherView()
        {
           
        }


        /// <summary>
        /// Method to create an add new weather view model
        /// </summary>
        public void AddNewWeather()
        {
            SetActiveViewModel(_newWeatherViewModel);
        }

        /// <summary>
        /// Sets the current view model and associated view to the main window's
        /// content
        /// </summary>
        /// <param name="viewModel"></param>
        public void SetActiveViewModel(ViewModelBase viewModel)
        {
            if (ViewModels.Contains(viewModel))
            {
                CurrentViewModel = viewModel;
            }
        }

        #endregion

        #region Events

        #endregion
    }
}
