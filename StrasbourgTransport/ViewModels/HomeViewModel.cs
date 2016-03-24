using GalaSoft.MvvmLight;
using Huyn.Utils;
using StrasbourgTransport.Common;
using StrasbourgTransport.Models;
using StrasbourgTransport.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace StrasbourgTransport.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private bool _sResultVisible;
        public bool IsResultVisible
        {
            get { return _sResultVisible; }
            set
            {
                _sResultVisible = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<StopResult> StopResults { get; private set; }
        public ObservableCollection<JourneyResult> TramResults { get; private set; }
        public ObservableCollection<JourneyResult> BusResults { get; private set; }

        public string StopSearch { get; set; }

        private StopResult _currentStop;
        public StopResult CurrentStop
        {
            get
            {
                return _currentStop;
            }
            set
            {
                _currentStop = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand<AutoSuggestBoxTextChangedEventArgs> GetStopResultsCommand { private set; get; }
        public RelayCommand<AutoSuggestBoxSuggestionChosenEventArgs> GetJourneyResultsCommand { private set; get; }
        public RelayCommand PinStopCommand { get; private set; }

        private readonly Delayer searchStopsWithDelay = new Delayer(TimeSpan.FromSeconds(0.5));

        public HomeViewModel(IDataService dataService)
        {
            _dataService = dataService;

            StopResults = new ObservableCollection<StopResult>();
            TramResults = new ObservableCollection<JourneyResult>();
            BusResults = new ObservableCollection<JourneyResult>();

            searchStopsWithDelay.Action += async (sender, e) =>
            {
                await GetStopsCodeByName(StopSearch);
            };

            GetStopResultsCommand = new RelayCommand<AutoSuggestBoxTextChangedEventArgs>(args =>
            {
                if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput && StopSearch.Length > 2)
                {
                    searchStopsWithDelay.ResetAndTick();
                }
            });

            GetJourneyResultsCommand = new RelayCommand<AutoSuggestBoxSuggestionChosenEventArgs>(async stop =>
            {
                var current = (StopResult)stop.SelectedItem;
                current.Name = current.Name.ToUpper();

                this.CurrentStop = current;

                await GetJourneyResults(CurrentStop.Code);
            });

            PinStopCommand = new RelayCommand(() =>
            {
                if (CurrentStop == null)
                    return;

                ApplicationDataContainer settingsRoaming = ApplicationData.Current.RoamingSettings;
                ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();

                var key = $"{this.CurrentStop.Code}/{this.CurrentStop.Name}";

                composite["Code"] = this.CurrentStop.Code;
                composite["Name"] = this.CurrentStop.Name;

                if (!settingsRoaming.Values.Keys.Contains(key))
                {
                    settingsRoaming.Values[key] = composite;
                }
            });
        }

        private async Task GetStopsCodeByName(string stopName)
        {
            var stops = await _dataService.GetStopsByName(stopName);

            StopResults.Clear();
            foreach (var stop in stops)
            {
                StopResults.Add(stop);
            }
        }

        internal async Task GetJourneyResults(string stopCode)
        {
            var journeys = await _dataService.GetJourneys(stopCode);

            foreach (var journey in journeys)
            {
                if (journey.Mode.Contains("Tram"))
                {
                    TramResults.Add(journey);
                }
                else if (journey.Mode.Contains("Bus"))
                {
                    BusResults.Add(journey);
                }
            }

            IsResultVisible = true;
        }
    }
}

