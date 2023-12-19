using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighwayToPeak.Models.Contracts;

namespace HighwayToPeak.Models
{
    public class Peak : IPeak
    {
        private string peak;
        private int elevation;

        public Peak(string peak, int elevation, string difficultyLevel)
        {
            Name = peak;
            Elevation = elevation;
            DifficultyLevel = difficultyLevel;
        }
        public string Name
        {
            get => peak;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Peak name cannot be null or whitespace.");
                }
                peak = value;
            }
        }

        public int Elevation
        {
            get => elevation; 
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Peak elevation must be a positive value.");
                }
                elevation = value;
            }
        }
        public string DifficultyLevel { get; }

        public override string ToString()
        {
            return $"Peak: {Name} -> Elevation: {Elevation}, Difficulty: {DifficultyLevel}";
        }
    }
}
