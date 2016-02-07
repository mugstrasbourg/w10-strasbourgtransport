using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrasbourgTransport.Models
{
    public class JourneyResult
    {

        public string Mode
        {
            get; set;
        }

        public string Direction
        {
            get; set;
        }

        public string Time
        {
            get; set;
        }
        public Ligne Ligne { get; set; }
    }
}
