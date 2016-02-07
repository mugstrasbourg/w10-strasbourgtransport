using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrasbourgTransport.Models
{
    public class StopResult
    {
        public string Code
        {
            get; set;
        }

        private string _name;

        public string Name
        {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }
    }
}
