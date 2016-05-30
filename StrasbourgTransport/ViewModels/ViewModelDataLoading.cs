using GalaSoft.MvvmLight;

namespace StrasbourgTransport.ViewModels
{
    public class ViewModelDataLoading : ViewModelBase
    {
        private bool _isDataLoading;
        public bool IsDataLoading
        {
            get
            {
                return _isDataLoading;
            }
            protected set
            {
                if (value != _isDataLoading)
                {
                    _isDataLoading = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
