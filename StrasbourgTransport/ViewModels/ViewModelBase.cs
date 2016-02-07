using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StrasbourgTransport.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool isDataLoading;
        public bool IsDataLoading
        {
            get
            {
                return isDataLoading;
            }
            set
            {
                if (isDataLoading != value)
                {
                    isDataLoading = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]String propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
