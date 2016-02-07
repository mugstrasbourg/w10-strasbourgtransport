using StrasbourgTransport.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace StrasbourgTransport.ViewModels
{
    public class FavorisViewModel
    {
        public ObservableCollection<StopResult> FavoriteStops { get; set; }

        public FavorisViewModel()
        {
            this.FavoriteStops = new ObservableCollection<StopResult>();
            ApplicationDataContainer settingsRoaming = ApplicationData.Current.RoamingSettings;

            foreach (var item in settingsRoaming.Values)
            {
                var composite = item.Value as ApplicationDataCompositeValue;

                this.FavoriteStops.Add(new StopResult {
                    Code = composite["Code"] as string,
                    Name = composite["Name"] as string
                });
            }
        }
    }
}
