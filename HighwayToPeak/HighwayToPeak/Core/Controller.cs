using System.Text;
using HighwayToPeak.Core.Contracts;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            peaks = new PeakRepository();
            climbers = new ClimberRepository();
            baseCamp = new BaseCamp();
        }

        IRepository<IPeak> peaks;
        IRepository<IClimber> climbers;
        IBaseCamp baseCamp;


        public string AddPeak(string name, int elevation, string difficultyLevel)
        {
            Peak peak = new Peak(name, elevation, difficultyLevel);

            var peakName = peaks.Get(name);

            if (peaks.All.Contains(peakName))
            {
                return $"{name} is already added as a valid mountain destination.";
            }

            if (!IsValidDifficultyLevel(difficultyLevel))
            {
                return $"{difficultyLevel} peaks are not allowed for international climbers.";
            }

            peaks.Add(peak);
            return $"{name} is allowed for international climbing. See details in {nameof(PeakRepository)}.";
        }

        public string NewClimberAtCamp(string name, bool isOxygenUsed)
        {
            IClimber climber;

            var climberName = climbers.Get(name);
            if (climbers.All.Contains(climberName))
            {
                return $"{name} is a participant in {nameof(ClimberRepository)} and cannot be duplicated.";
            }

            if (isOxygenUsed)
            {
                climber = new OxygenClimber(name);
                climbers.Add(climber);
                baseCamp.ArriveAtCamp(name);
            }
            else if (!isOxygenUsed)
            {
                climber = new NaturalClimber(name);
                climbers.Add(climber);
                baseCamp.ArriveAtCamp(name);
            }

            return $"{name} has arrived at the BaseCamp and will wait for the best conditions.";
        }

        public string AttackPeak(string climberName, string peakName)
        {
            var peak = peaks.Get(peakName);
            IClimber climber = climbers.Get(climberName);

            if (!climbers.All.Contains(climber))
            {
                return $"Climber - {climberName}, has not arrived at the BaseCamp yet.";
            }

            var nameOfPeak = peaks.Get(peakName);
            if (!peaks.All.Contains(nameOfPeak))
            {
                return $"{peakName} is not allowed for international climbing.";
            }

            if (!baseCamp.Residents.Contains(climber.Name))
            {
                return $"{climberName} not found for gearing and instructions. The attack of {peakName} will be postponed.";
            }

            if (peak.DifficultyLevel == "Extreme" && climber is NaturalClimber)
            {
                return $"{climberName} does not cover the requirements for climbing {peakName}.";
            }
            baseCamp.LeaveCamp(climberName);
            climber.Climb(peak);

            if (climber.Stamina > 0)
            {
                baseCamp.ArriveAtCamp(climberName);
                return $"{climberName} successfully conquered {peakName} and returned to BaseCamp.";
            }
            else // climber.Stamina <= 0
            {
                return $"{climberName} did not return to BaseCamp.";
            }
        }

        public string CampRecovery(string climberName, int daysToRecover)
        {
            if(!baseCamp.Residents.Contains(climberName))
            {
                return $"{climberName} not found at the BaseCamp.";
            }

            var climber = climbers.Get(climberName);

            if (climber.Stamina == 10)
            {
                return $"{climberName} has no need of recovery.";
            }
            if (climber.Stamina < 10) 
            {
                climber.Rest(daysToRecover);
            }
            return $"{climberName} has been recovering for {daysToRecover} days and is ready to attack the mountain.";
        }

        public string BaseCampReport()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("BaseCamp residents:");

            if (baseCamp.Residents.Count > 0)
            {
                foreach (var climber in baseCamp.Residents)
                {
                    result.AppendLine($"Name: {climber}, Stamina: {climbers.Get(climber).Stamina}, Count of Conquered Peaks: {climbers.Get(climber).ConqueredPeaks.Count}");
                }
            }
            else
            {
                return "BaseCamp is currently empty.";
            }

            return result.ToString().TrimEnd();
        }

        public string OverallStatistics()
        {
            var climbersList = climbers.All.OrderByDescending(climber => climber.ConqueredPeaks.Count)
                .ThenBy(climber => climber.Name)
                .ToList();

            StringBuilder result = new StringBuilder();

            result.AppendLine("***Highway-To-Peak***");

            foreach (var climber in climbersList)
            {
                result.AppendLine(climber.ToString());

                var conqueredPeaksList = climber.ConqueredPeaks
                    .Select(peakName => peaks.Get(peakName))
                    .OrderByDescending(peak => peak.Elevation)
                    .ToList();

                foreach (var conqueredPeak in conqueredPeaksList)
                {
                    result.AppendLine(conqueredPeak.ToString());
                }
            }


            return result.ToString().TrimEnd();
        }
        private bool IsValidDifficultyLevel(string difficultyLevel)
        {
            var acceptedDifficultyLevels = new List<string> { "Extreme", "Hard", "Moderate" };
            return acceptedDifficultyLevels.Contains(difficultyLevel);
        }

    }
}
