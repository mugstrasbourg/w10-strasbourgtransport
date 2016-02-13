using StrasbourgTransport.Models;
using StrasbourgTransport.Services;
using System.Collections.ObjectModel;

namespace StrasbourgTransport.ViewModels
{
    public class InfoTraficViewModel : ViewModelDataLoading
    {
        private readonly IDataService _dataService;

        public ObservableCollection<TrafficInfoResult> TrafficInfoResults
        {
            get; private set;
        }

        public InfoTraficViewModel(IDataService dataService)
        {
            _dataService = dataService;
            TrafficInfoResults = new ObservableCollection<TrafficInfoResult>();
        }

        public async void GetTrafficInfoData()
        {
            IsDataLoading = true;

            var trafficInfoList = await _dataService.GetTrafficInfo();
            if (trafficInfoList != null)
            {
                TrafficInfoResults.Clear();
                foreach (var trafficInfo in trafficInfoList)
                {
                    TrafficInfoResults.Add(trafficInfo);
                }
            }

            IsDataLoading = false;
        }
    }
}
