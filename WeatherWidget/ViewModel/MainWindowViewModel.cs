using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        //ok/cancel for add New weather
        private RelayCommand _okNewWeatherCommand;
        private RelayCommand _cancelNewWeatherCommand;
        //show/hide settings

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
                       (_showWeatherViewCommand = new RelayCommand(param => ShowWeatherView(),
                           param=>CanExecuteShowWeather()));
            }
        }

        public RelayCommand OkNewWeatherCommand
        {
            get
            {
                return _okNewWeatherCommand ?? (_okNewWeatherCommand = 
                    new RelayCommand(param => OKAddNewWeather(), param=>CanExecuteAddNew()));
            }
        }

        public RelayCommand CancelNewWeatherCommand
        {
            get
            {
                return _cancelNewWeatherCommand ??
                       (_cancelNewWeatherCommand = new RelayCommand(param => CancelAddNewWeather(),
                           param=>CanExecuteShowWeather()));
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
            _newWeatherViewModel = new AddNewWeatherViewModel(OkNewWeatherCommand, CancelNewWeatherCommand);
            _weatherCollectionViewModel = new WeatherCollectionViewModel();
            _viewModels.Add(_newWeatherViewModel);
            _viewModels.Add(_weatherCollectionViewModel);
            SetActiveViewModel(_newWeatherViewModel);
        }

        public MainWindowViewModel(WeatherSettings settings)
        {
            _viewModels = new ObservableCollection<ViewModelBase>();
            _newWeatherViewModel = new AddNewWeatherViewModel(OkNewWeatherCommand, CancelNewWeatherCommand);
            _weatherCollectionViewModel = new WeatherCollectionViewModel(settings);
            _viewModels.Add(_newWeatherViewModel);
            _viewModels.Add(_weatherCollectionViewModel);
            if (_weatherCollectionViewModel.WeatherItems.Any())
            {
                SetActiveViewModel(_weatherCollectionViewModel);
            }
            else
            {
                SetActiveViewModel(_newWeatherViewModel);
            }

        }


        #endregion

        #region Methods
        /// <summary>
        /// Switches the current view model to the
        /// weather collectin view model
        /// </summary>
        public void ShowWeatherView()
        {
           SetActiveViewModel(_weatherCollectionViewModel);
            _weatherCollectionViewModel.StartUpdating();
        }

        public bool CanExecuteShowWeather()
        {
            return (_weatherCollectionViewModel.WeatherItems.Any() 
                && !CurrentViewModel.Equals(_weatherCollectionViewModel));
        }

        public void OKAddNewWeather()
        {
            var location = _newWeatherViewModel.SelectedLocation;
            var newViewModel = new WeatherViewModel(location);
            _weatherCollectionViewModel.AddNewWeatherItem(newViewModel);
            ShowWeatherView();
        }

        public bool CanExecuteAddNew()
        {
           return (_newWeatherViewModel.SelectedLocation != null);
        }

        public void CancelAddNewWeather()
        {
            ShowWeatherView();
        }

        /// <summary>
        /// Method to create an add new weather view model
        /// </summary>
        public void AddNewWeather()
        {
            _weatherCollectionViewModel.StopUpdating();
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
