using System.Collections.Generic;
using System.Threading.Tasks;
using StrasbourgTransport.Models;

namespace StrasbourgTransport.Services
{
    public interface IDataService
    {
        Task<IList<JourneyResult>> GetJourneys(string stopCode);
        Task<IList<StopResult>> GetStopsByName(string stopName);
        Task<IList<TrafficInfoResult>> GetTrafficInfo();
    }
}