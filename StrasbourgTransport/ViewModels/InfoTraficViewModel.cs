using StrasbourgTransport.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Text;

namespace StrasbourgTransport.ViewModels
{
    public class InfoTraficViewModel : ViewModelBase
    {
        public ObservableCollection<TrafficInfoResult> TrafficInfoResults
        {
            get; private set;
        }

        private Random _randomEngine;

        public InfoTraficViewModel()
        {
            TrafficInfoResults = new ObservableCollection<TrafficInfoResult>();

            _randomEngine = new Random();
        }

        public async void GetTrafficInfoData()
        {
            IsDataLoading = true;

            HttpClient httpClient = new HttpClient();

            var httpResponse = await httpClient.GetAsync("http://twitrss.me/twitter_user_to_rss/?user=CTS_Infostrafic");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                var trafficInfoList = XDocument.Parse(content).Descendants("item").Select(i =>
                {
                    var randomBiggerTweet = IsFontBigger();

                    return new TrafficInfoResult
                    {
                        Title = i.Element("title").Value,
                        FontSize = randomBiggerTweet ? 22 : 15,
                        Weight = randomBiggerTweet ? FontWeights.Bold : FontWeights.Normal
                    };
                }).ToList();

                TrafficInfoResults.Clear();
                foreach (var trafficInfo in trafficInfoList)
                {
                    TrafficInfoResults.Add(trafficInfo);
                }
            }

            IsDataLoading = false;
        }

        // Randomly say if the font is bigger than the "normal size"
        // Twitter-like effect
        public bool IsFontBigger()
        {
            return _randomEngine.Next(0, 11) > 8;
        }
    }
}
