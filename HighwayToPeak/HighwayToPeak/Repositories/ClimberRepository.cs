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
    public class ClimberRepository : IRepository<IClimber>
    {
        private readonly List<IClimber> climbers;

        public ClimberRepository()
        {
            climbers = new List<IClimber>();
        }

        public IReadOnlyCollection<IClimber> All => new ReadOnlyCollection<IClimber>(climbers);
        public void Add(IClimber model)
        {
            climbers.Add(model);
        }

        public IClimber Get(string name)
        {
            return climbers.FirstOrDefault(climber => climber.Name == name);
        }
    }
}
