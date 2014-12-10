using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWidget.APIConnect;
using WeatherWidget.Model;

namespace WeatherWidget.ViewModel
{
    public class AddNewWeatherViewModel : ViewModelBase
    {
        #region Fields

        private RelayCommand _okCommand;
        private RelayCommand _cancelCommand;
        private RelayCommand _searchCommand;
        private RelayCommand _selectLocationCommand;
        private LocationSearcher _searcher;

        private bool _searchEnabled = true;
        private bool _nameBoxEnabled = true;
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

        public RelayCommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new RelayCommand(Search)); }
        }

        public RelayCommand SelectLocationCommand { get
        {
            return _selectLocationCommand ?? (_selectLocationCommand = new RelayCommand(SelectLocation));
        } }

        public bool SearchEnabled
        {
            get { return _searchEnabled; }
            set { _searchEnabled = value; OnPropertyChanged(); }
        }

        public bool NameBoxEnabled
        {
            get { return _nameBoxEnabled; }
            set { _nameBoxEnabled = value; OnPropertyChanged(); }
        }

        public Location SelectedLocation { get; set; }

        public String CityName { get; set; }

        public ObservableCollection<Location> Locations { get; set; }
        #endregion

        #region Constructor

        public AddNewWeatherViewModel(RelayCommand okCommand, RelayCommand cancelCommand)
        {
            Locations = new ObservableCollection<Location>();
            _okCommand = okCommand;
            _cancelCommand = cancelCommand;
            _searcher = new LocationSearcher();
        }

        #endregion

        #region Methods

        public async void Search(object param)
        {
            Locations.Clear();
            SearchEnabled = false;
            NameBoxEnabled = false;
            if (!(param is String))
            {
                return;
            }
            var cityName = param.ToString();

            _searcher.GenerateSearchQuery(cityName);

            var xml = await WeatherRequester.RequestXmlFromApi(_searcher.QueryString);

            if (xml != null)
            {
                var list = _searcher.ParseLocationXml(xml);
                foreach (var location in list)
                {
                    Locations.Add(location);
                }
            }
            SearchEnabled = true;
            NameBoxEnabled = true;

        }

        public void SelectLocation(object param)
        {
            if (!(param is Location))
            {
                return;
            }
            var location = (Location) param;

            SelectedLocation = location;

            foreach (var loc in Locations)
            {
                if (!loc.Equals(SelectedLocation))
                {
                    loc.IsChecked = false;
                }
            }
        }

        #endregion

        #region Events

        #endregion
    }
}
