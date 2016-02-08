using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;

namespace StrasbourgTransport.Models
{
    public class TrafficInfoResult
    {
        public string Title
        {
            get; set;
        }

        public DateTime Date
        {
            get; set;
        }

        public int FontSize { get; set; }
        public FontWeight Weight { get; set; }
    }
}
