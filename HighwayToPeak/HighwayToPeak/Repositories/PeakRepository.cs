using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Repositories
{
    public class PeakRepository : IRepository<IPeak>
    {
        private List<IPeak> peaks;

        public PeakRepository()
        {
            peaks = new List<IPeak>();
        }

        public IReadOnlyCollection<IPeak> All => peaks.AsReadOnly();
        public void Add(IPeak model)
        {
            peaks.Add(model);
        }

        public IPeak Get(string name)
        {
            return peaks.FirstOrDefault(peak => peak.Name == name);
        }
    }
}
