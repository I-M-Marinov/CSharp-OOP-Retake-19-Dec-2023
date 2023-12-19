using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HighwayToPeak.Models.Contracts;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber

    {
        private string name;
        private int stamina;
        private readonly List<string> conqueredPeaks;


        protected Climber(string name, int stamina)
        {
            Name = name;
            Stamina = stamina;
            conqueredPeaks = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Climber's name cannot be null or whitespace.");
                }
                name = value;
            }
        }

        public int Stamina
        {
            get => stamina;
            protected set
            {
                if (value > 10)
                {
                    value = 10;
                }

                if (value < 0)
                {
                    value = 0;
                }
                stamina = value;
            }
        }
        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks.AsReadOnly();
        public void Climb(IPeak peak)
        {
            var peakName = peak.Name;
            if (!conqueredPeaks.Contains(peakName))
            {
                conqueredPeaks.Add(peakName);
            }

            if (peak.DifficultyLevel == "Extreme")
            {
                Stamina -= 6;
            }
            else if (peak.DifficultyLevel == "Hard")
            {
                Stamina -= 4;
            }
            else if (peak.DifficultyLevel == "Moderate")
            {
                Stamina -= 2;
            }

        }

        public abstract void Rest(int daysCount);

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"{GetType().Name} - Name: {Name}, Stamina: {Stamina}");
            result.Append("Peaks conquered: ");

            if (ConqueredPeaks.Count > 0)
            {
                result.Append($"{ConqueredPeaks.Count}");
            }
            else
            {
                result.Append("no peaks conquered");
            }
            return result.ToString().TrimEnd();
        }
    }
}
