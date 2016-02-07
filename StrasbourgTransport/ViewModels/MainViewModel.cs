using StrasbourgTransport.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrasbourgTransport.CtsService;
using StrasbourgTransport.Services;
using System.ServiceModel;
using StrasbourgTransport.Common;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.ComponentModel;

namespace StrasbourgTransport.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _sResultVisible;
        public bool IsResultVisible
        {
            get { return _sResultVisible; }
            set
            {
                _sResultVisible = value;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
            }
        }


        public RelayCommand<AutoSuggestBoxTextChangedEventArgs> GetStopResultsCommand { private set; get; }
        public RelayCommand<AutoSuggestBoxSuggestionChosenEventArgs> GetJourneyResultsCommand { private set; get; }
        public RelayCommand PinStopCommand { get; private set; }

        public MainViewModel()
        {
            StopResults = new ObservableCollection<StopResult>();
            TramResults = new ObservableCollection<JourneyResult>();
            BusResults = new ObservableCollection<JourneyResult>();

            GetStopResultsCommand = new RelayCommand<AutoSuggestBoxTextChangedEventArgs>(async args =>
            {
                if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput && StopSearch.Length > 2)
                {
                    await GetStopsCodeByName(StopSearch);
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
            ServiceSoapClient client = new ServiceSoapClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(new CtsMessageHeader());
                var result = await client.rechercherCodesArretsDepuisLibelleAsync(stopName, 1);

                StopResults.Clear();
                foreach (var arret in result.ListeArret)
                {
                    StopResults.Add(new StopResult
                    {
                        Code = arret.Code,
                        Name = arret.Libelle
                    });

                }
            }
        }

        internal async Task GetJourneyResults(string stopCode)
        {
            ServiceSoapClient client = new ServiceSoapClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(new CtsMessageHeader());
                var result = await client.rechercheProchainesArriveesWebAsync(stopCode, 0, DateTime.Now.ToString("HH:mm"), 3);

                TramResults.Clear();
                BusResults.Clear();

                var lignes = Ligne.GetLignes();

                foreach (var journey in result.ListeArrivee)
                {
                    int indexWhiteSpace = journey.Destination.IndexOf(" ");
                    string number = journey.Destination.Substring(0, indexWhiteSpace);

                    JourneyResult journeyResult = new JourneyResult
                    {
                        Direction = journey.Destination.Remove(0, indexWhiteSpace).Trim(),
                        Mode = journey.Mode,
                        Time = journey.Horaire.Remove(journey.Horaire.Length - 3).Replace(':', 'h'),
                        Ligne = lignes.Single(ligne => ligne.Number.Equals(number))
                    };

                    if (journey.Mode.Contains("Tram"))
                    {
                        TramResults.Add(journeyResult);
                    }
                    else if (journey.Mode.Contains("Bus"))
                    {
                        BusResults.Add(journeyResult);
                    }
                }

                this.IsResultVisible = true;
            }
        }
    }
}
