using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherWidget.Annotations;

namespace WeatherWidget
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields

        #endregion

        #region Properties
        public string DisplayName { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor

        #endregion

        #region Methods

        #endregion

        #region Events

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
