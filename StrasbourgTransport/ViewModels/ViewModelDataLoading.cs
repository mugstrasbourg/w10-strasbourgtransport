using GalaSoft.MvvmLight;
using System.Xml.Serialization;

namespace StrasbourgTransport.ViewModels
{
    public class ViewModelDataLoading : ViewModelBase
    {
        [XmlIgnore]
        private bool _isDataLoading;

        [XmlIgnore]
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
