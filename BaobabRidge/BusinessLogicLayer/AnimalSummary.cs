using System.Globalization;

namespace BaobabRidge.BusinessLogicLayer
{
    public class AnimalSummary
    {
        public int TotalAnimals { get; }
        public decimal? AverageAge { get; }
        public decimal? AverageRecoveryScore { get; }
        public Dictionary<string, int> StatusCounts { get; } = new Dictionary<string, int>
        {
            { "Critical", 0 }, { "Serious", 0 }, { "Stable", 0 },
            { "Recovering", 0 }, { "Release-Ready", 0 }
        };

        // Calculates the statistics from the valid records just loaded from animals.txt.
        public AnimalSummary(List<Animal> animals)
        {
            TotalAnimals = animals.Count;
            decimal ages = 0;
            decimal scores = 0;
            foreach (Animal animal in animals)
            {
                ages += animal.Age;
                scores += animal.RecoveryScore;
                StatusCounts[animal.Status]++;
            }
            if (TotalAnimals > 0)
            {
                AverageAge = ages / TotalAnimals;
                AverageRecoveryScore = scores / TotalAnimals;
            }
        }

        // Produces the brief's exact report layout, independent of Windows regional settings.
        public string BuildReport(DateTime generated)
        {
            List<string> lines = new List<string>
            {
                "BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE",
                "ANIMAL SUMMARY REPORT",
                "Generated: " + generated.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                "----------------------------------------",
                "Total animals:".PadRight(26) + TotalAnimals.ToString(CultureInfo.InvariantCulture),
                "Average age (years):".PadRight(26) + (AverageAge?.ToString("F2", CultureInfo.InvariantCulture) ?? "N/A"),
                "Average recovery score:".PadRight(26) + (AverageRecoveryScore?.ToString("F2", CultureInfo.InvariantCulture) ?? "N/A"),
                "----------------------------------------",
                "Animals per status:"
            };
            foreach (string status in new[] { "Critical", "Serious", "Stable", "Recovering", "Release-Ready" })
            {
                lines.Add(("  " + status + ":").PadRight(26) + StatusCounts[status].ToString(CultureInfo.InvariantCulture));
            }
            return string.Join(Environment.NewLine, lines) + Environment.NewLine;
        }
    }
}
