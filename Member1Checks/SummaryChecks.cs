using System.Globalization;
using System.Text;
using BaobabRidge.BusinessLogicLayer;
using BaobabRidge.DataLayer;
using BaobabRidge.PresentationLayer;

internal static partial class Program
{
    // Checks exact output, zero counts, rounding and culture without relying on the UI.
    private static void CheckSummaryFiles()
    {
        DateTime generated = new DateTime(2026, 10, 20, 9, 32, 55);
        List<Animal> animals = new List<Animal>();
        for (int i = 0; i < 5; i++)
        {
            Animal animal = new Animal("WR-" + (400 + i).ToString("D4"), "Animal", "Serval", i + 1, i * 20);
            AnimalClassifier.Classify(animal);
            animals.Add(animal);
        }
        string expected = string.Join(Environment.NewLine, new[]
        {
            "BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE", "ANIMAL SUMMARY REPORT",
            "Generated: 2026-10-20 09:32", "----------------------------------------",
            "Total animals:            5", "Average age (years):      3.00",
            "Average recovery score:   40.00", "----------------------------------------", "Animals per status:",
            "  Critical:               1", "  Serious:                1", "  Stable:                 1",
            "  Recovering:             1", "  Release-Ready:          1", ""
        });
        CultureInfo previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
            Check(new AnimalSummary(animals).BuildReport(generated) == expected, "Exact summary layout and invariant decimal/date formatting");
        }
        finally { CultureInfo.CurrentCulture = previous; }
        AnimalSummary empty = new AnimalSummary(new List<Animal>());
        string emptyReport = empty.BuildReport(generated);
        Check(empty.TotalAnimals == 0 && empty.AverageAge == null && empty.AverageRecoveryScore == null,
            "Empty summary has zero total and undefined averages");
        Check(emptyReport.Contains("Average age (years):      N/A") && emptyReport.Contains("Average recovery score:   N/A")
            && empty.StatusCounts.Count == 5 && empty.StatusCounts.Values.All(count => count == 0), "Empty report lists N/A and all five zero counts");
        AnimalSummary single = new AnimalSummary(new List<Animal> { animals[0] });
        Check(single.StatusCounts["Critical"] == 1 && single.StatusCounts.Values.Count(value => value == 0) == 4,
            "Statuses with no animals are retained");
        AnimalSummary rounded = new AnimalSummary(new List<Animal> { animals[0], animals[0], animals[1] });
        Check(rounded.BuildReport(generated).Contains("Average age (years):      1.33")
            && rounded.BuildReport(generated).Contains("Average recovery score:   6.67"), "Fractional means round to two places");

        string folder = Path.Combine(testFolder, "summary-files");
        Directory.CreateDirectory(folder);
        FileHandler handler = new FileHandler(Path.Combine(folder, "animals.txt"));
        string reportPath = Path.Combine(folder, "summary.txt");
        Check(handler.TryWriteSummary(expected, out _) && File.ReadAllText(reportPath) == expected, "Create summary beside data file");
        Check(!File.ReadAllBytes(reportPath).Take(3).SequenceEqual(Encoding.UTF8.GetPreamble()), "Summary uses UTF-8 without a BOM");
        Check(handler.TryWriteSummary(emptyReport, out _) && File.ReadAllText(reportPath) == emptyReport, "Regenerate overwrites rather than appends");
        File.SetAttributes(reportPath, FileAttributes.ReadOnly);
        try
        {
            Check(!handler.TryWriteSummary(expected, out string message) && message.Contains("read-only")
                && File.ReadAllText(reportPath) == emptyReport, "Read-only report gives friendly error and preserves previous report");
        }
        finally { File.SetAttributes(reportPath, FileAttributes.Normal); }
        using (FileStream locked = new FileStream(reportPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
        {
            Check(!handler.TryWriteSummary(expected, out string message) && message.Contains("locking"), "Locked summary gives friendly error");
        }
        Check(File.ReadAllText(reportPath) == emptyReport, "Failed report write preserves previous bytes");
        Check(!new FileHandler(Path.Combine(folder, "missing", "animals.txt")).TryWriteSummary(expected, out string missingMessage)
            && missingMessage.Contains("folder"), "Missing summary folder handled");
        Check(Directory.GetFiles(folder, "*.tmp").Length == 0, "Report failures clean temporary files");
    }

    // Exercises the real button with external file changes and save/read failures.
    private static void CheckSummaryForm(string? imagePath)
    {
        string animalPath = Path.Combine(AppContext.BaseDirectory, "animals.txt");
        string summaryPath = Path.Combine(AppContext.BaseDirectory, "summary.txt");
        byte[] originalAnimals = File.ReadAllBytes(animalPath);
        byte[]? originalSummary = File.Exists(summaryPath) ? File.ReadAllBytes(summaryPath) : null;
        try
        {
            using MainForm form = new MainForm();
            form.ShowInTaskbar = false;
            form.Show();
            Application.DoEvents();
            Button generate = (Button)form.Controls.Find("btnGenerateSummary", true)[0];
            TextBox display = (TextBox)form.Controls.Find("txtSummary", true)[0];
            Label feedback = (Label)form.Controls.Find("lblFeedback", true)[0];
            Check(generate.Visible && display.ReadOnly && display.Multiline, "Summary is reachable and displayed on the form in a read-only box");
            generate.PerformClick();
            Check(display.Text == File.ReadAllText(summaryPath) && display.Text.Contains("Total animals:            12"), "UI and saved starter summary agree");
            if (imagePath != null)
            {
                using Bitmap preview = new Bitmap(form.Width, form.Height);
                form.DrawToBitmap(preview, new Rectangle(0, 0, form.Width, form.Height));
                preview.Save(imagePath);
            }
            File.WriteAllText(animalPath, "bad line" + Environment.NewLine
                + "WR-0999|Fresh|Serval|10|80|Release-Ready|Pre-Release Camp" + Environment.NewLine);
            generate.PerformClick();
            Check(display.Text.Contains("Total animals:            1") && display.Text.Contains("Average age (years):      10.00")
                && feedback.Text.Contains("Skipped 1 malformed"), "Summary reads current disk data rather than stale grid and reports skipped lines");
            File.WriteAllText(animalPath, string.Empty);
            generate.PerformClick();
            Check(display.Text.Contains("Total animals:            0") && display.Text.Contains("N/A"), "Empty summary works through UI");
            string savedReport = File.ReadAllText(summaryPath);
            using (FileStream locked = new FileStream(summaryPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                generate.PerformClick();
                Check(feedback.Text.Contains("was not saved") && display.Text.Contains("ANIMAL SUMMARY REPORT"), "UI distinguishes displayed statistics from failed report save");
            }
            Check(File.ReadAllText(summaryPath) == savedReport, "UI write failure preserves report");
            using (FileStream locked = new FileStream(animalPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                generate.PerformClick();
                Check(display.Text.Length == 0 && feedback.Text.Contains("locking"), "Read failure clears old summary and gives friendly feedback");
            }
            Check(File.ReadAllText(summaryPath) == savedReport, "Read failure does not overwrite report");
            File.Delete(animalPath);
            generate.PerformClick();
            Check(File.Exists(animalPath) && display.Text.Contains("Total animals:            0"), "Missing data file creates empty file for summary");
            form.Close();
        }
        finally
        {
            File.WriteAllBytes(animalPath, originalAnimals);
            if (originalSummary == null) File.Delete(summaryPath);
            else File.WriteAllBytes(summaryPath, originalSummary);
        }
    }
}
