using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrasbourgTransport.Models
{
    public class Ligne
    {
        public string Color { get; set; }
        public string Number { get; set; }

        public static List<Ligne> GetLignes()
        {
            return new List<Ligne>() {
    new Ligne() {
        Number = "G", Color = "000099"
    }, new Ligne() {
        Number = "4", Color = "CC071E"
    }, new Ligne() {
        Number = "17", Color = "F7DDDD"
    }, new Ligne() {
        Number = "19", Color = "F7C9E3"
    }, new Ligne() {
        Number = "6", Color = "7D5D9F"
    }, new Ligne() {
        Number = "A", Color = "E2001A"
    }, new Ligne() {
        Number = "D", Color = "009932"
    }, new Ligne() {
        Number = "E", Color = "9085BA"
    }, new Ligne() {
        Number = "10", Color = "FABB00"
    }, new Ligne() {
        Number = "13", Color = "F7C9E3"
    }, new Ligne() {
        Number = "15", Color = "004A99"
    }, new Ligne() {
        Number = "12", Color = "89CCCF"
    }, new Ligne() {
        Number = "22", Color = "89CCCF"
    }, new Ligne() {
        Number = "15a", Color = "89CCCF"
    }, new Ligne() {
        Number = "50", Color = "FFDD00"
    }, new Ligne() {
        Number = "50A", Color = "FFDD00"
    }, new Ligne() {
        Number = "2", Color = "E2001A"
    }, new Ligne() {
        Number = "B", Color = "009EE0"
    }, new Ligne() {
        Number = "F", Color = "B1C800"
    }, new Ligne() {
        Number = "14", Color = "89CCCF"
    }, new Ligne() {
        Number = "24", Color = "89CCCF"
    }, new Ligne() {
        Number = "63", Color = "F7C9DD"
    }, new Ligne() {
        Number = "62", Color = "F7C9E3"
    }, new Ligne() {
        Number = "65", Color = "F7C9E3"
    }, new Ligne() {
        Number = "30", Color = "FFDD00"
    }, new Ligne() {
        Number = "40", Color = "FFDD00"
    }, new Ligne() {
        Number = "40", Color = "FFDD00"
    }, new Ligne() {
        Number = "7", Color = "009036"
    }, new Ligne() {
        Number = "C", Color = "F29400"
    }, new Ligne() {
        Number = "21", Color = "89CCCF"
    }, new Ligne() {
        Number = "31", Color = "F7C9E3"
    }, new Ligne() {
        Number = "72", Color = "F7C9DD"
    }, new Ligne() {
        Number = "71", Color = "89CCCF"
    }, new Ligne() {
        Number = "87", Color = "808080"
    }, new Ligne() {
        Number = "ROST", Color = "D0AACC"
    }, new Ligne() {
        Number = "NHC", Color = "D0AACC"
    }, new Ligne() {
        Number = "N1", Color = "E2001A"
    }, new Ligne() {
        Number = "N2", Color = "004A99"
    }, new Ligne() {
        Number = "N3", Color = "FABB00"
    }, new Ligne() {
        Number = "70", Color = "FABB00"
    }, new Ligne() {
        Number = "29", Color = "9FC204"
    }, new Ligne() {
        Number = "66", Color = "D0AACC"
    }, new Ligne() {
        Number = "27", Color = "9FC204"
    }, new Ligne() {
        Number = "88", Color = "808080",
    }, new Ligne() {
        Number = "89", Color = "808080"
    }, new Ligne() {
        Number = "NDFM", Color = "D0AACC"
    }
};
        }
    }
}
