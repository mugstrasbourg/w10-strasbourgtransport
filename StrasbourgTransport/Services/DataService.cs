using StrasbourgTransport.CtsService;
using StrasbourgTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Text;

namespace StrasbourgTransport.Services
{
    public class DataService : IDataService
    {
        private readonly Random _randomEngine = new Random();

        public async Task<IList<StopResult>> GetStopsByName(string stopName)
        {
            ServiceSoapClient client = new ServiceSoapClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(new CtsMessageHeader());
                var result = await client.rechercherCodesArretsDepuisLibelleAsync(new rechercherCodesArretsDepuisLibelleRequest
                {
                    NoPage = 1,
                    Saisie = stopName,
                });

                return result.rechercherCodesArretsDepuisLibelleResult.ListeArret.Select(la => new StopResult
                {
                    Code = la.Code,
                    Name = la.Libelle
                }).ToList();
            }
        }

        public async Task<IList<JourneyResult>> GetJourneys(string stopCode)
        {
            ServiceSoapClient client = new ServiceSoapClient();
            using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(new CtsMessageHeader());
                var result = await client.rechercheProchainesArriveesWebAsync(new rechercheProchainesArriveesWebRequest
                {
                    CodeArret = stopCode,
                    Heure = DateTime.Now.ToString("HH:mm"),
                    Mode = 0,
                    NbHoraires = 3
                });

                var lignes = Ligne.GetLignes();

                return result.rechercheProchainesArriveesWebResult.ListeArrivee.Select(journey =>
                {
                    int indexWhiteSpace = journey.Destination.IndexOf(" ");
                    string number = journey.Destination.Substring(0, indexWhiteSpace);

                    return new JourneyResult
                    {
                        Direction = journey.Destination.Remove(0, indexWhiteSpace).Trim(),
                        Mode = journey.Mode,
                        Time = journey.Horaire.Remove(journey.Horaire.Length - 3).Replace(':', 'h'),
                        Ligne = lignes.Single(ligne => ligne.Number.Equals(number))
                    };
                }).ToList();
            }
        }

        public const string _trafficInfoUrl = "http://twitrss.me/twitter_user_to_rss/?user=CTS_Infostrafic";

        public async Task<IList<TrafficInfoResult>> GetTrafficInfo()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync(_trafficInfoUrl);
                if (httpResponse.IsSuccessStatusCode)
                {
                    string content = await httpResponse.Content.ReadAsStringAsync();

                    return XDocument.Parse(content).Descendants("item").Select(i =>
                    {
                        var randomBiggerTweet = IsFontBigger();

                        return new TrafficInfoResult
                        {
                            Title = i.Element("title").Value,
                            FontSize = randomBiggerTweet ? 22 : 15,
                            Weight = randomBiggerTweet ? FontWeights.Bold : FontWeights.Normal
                        };
                    }).ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        // Randomly say if the font is bigger than the "normal size"
        // Twitter-like effect
        public bool IsFontBigger()
        {
            return _randomEngine.Next(0, 11) > 8;
        }
    }
}
