using System.Globalization;
using System.Text;
using BaobabRidge.BusinessLogicLayer;
using BaobabRidge.DataLayer;
using BaobabRidge.PresentationLayer;

internal static partial class Program
{
    private static int checks;
    private static readonly string testFolder = Path.Combine(AppContext.BaseDirectory, "BaobabRidgeChecks-" + Guid.NewGuid());

    // Runs repeatable checks in temporary files and returns a failing exit code on any mismatch.
    [STAThread]
    private static int Main(string[] args)
    {
        try
        {
            if (args.Length >= 2 && args[0] == "--layout-font")
            {
                // Run font-scaling checks in a separate process before any controls are created.
                Application.SetDefaultFont(new Font("Segoe UI", float.Parse(args[1], CultureInfo.InvariantCulture)));
                CheckScaledLayout(args.Length > 2 ? args[2] : null);
                Console.WriteLine("PASS: " + checks + " layout checks at font " + args[1] + ".");
                return 0;
            }
            Directory.CreateDirectory(testFolder);
            CheckClassification();
            CheckValidation();
            CheckFiles();
            CheckForm(args.Length > 0 ? args[0] : null);
            CheckUpdateFiles();
            CheckUpdateForm(args.Length > 0 ? args[0] : null);
            Console.WriteLine("PASS: " + checks + " checks.");
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine("FAIL: " + error.Message);
            return 1;
        }
        finally
        {
            // Delete only the uniquely named temporary directory created by this test run.
            if (Directory.Exists(testFolder))
            {
                Directory.Delete(testFolder, true);
            }
        }
    }

    // Asserts expected results at every classification boundary and rejects invalid scores.
    private static void CheckClassification()
    {
        int[] scores = { 0, 19, 20, 39, 40, 59, 60, 79, 80, 100 };
        string[] statuses = { "Critical", "Critical", "Serious", "Serious", "Stable", "Stable", "Recovering", "Recovering", "Release-Ready", "Release-Ready" };
        string[] housing = { "Intensive Care Unit", "Intensive Care Unit", "High-Dependency Ward", "High-Dependency Ward", "Recovery Ward", "Recovery Ward", "Outdoor Enclosure", "Outdoor Enclosure", "Pre-Release Camp", "Pre-Release Camp" };
        for (int index = 0; index < scores.Length; index++)
        {
            Animal animal = CreateAnimal("WR-0001", scores[index]);
            Check(animal.Status == statuses[index] && animal.HousingUnit == housing[index], "Classification at " + scores[index]);
        }
        Animal updated = CreateAnimal("WR-0001", 79);
        updated.RecoveryScore = 80;
        AnimalClassifier.Classify(updated);
        Check(updated.Status == "Release-Ready" && updated.HousingUnit == "Pre-Release Camp", "Shared classification supports a 79 to 80 update");
        foreach (int score in new[] { -1, 101 })
        {
            bool rejected = false;
            try
            {
                CreateAnimal("WR-0001", score);
            }
            catch (ArgumentOutOfRangeException)
            {
                rejected = true;
            }
            Check(rejected, "Classifier rejects " + score);
        }
    }

    // Exercises normalization, empty values, malformed IDs, text restrictions, and numeric limits.
    private static void CheckValidation()
    {
        Check(AnimalValidator.TryCreateAnimal(" wr-0007 ", " Naledi ", " Serval ", " 0 ", " 100 ", out Animal? normalized, out _, out _), "Valid normalized input");
        Check(normalized!.Id == "WR-0007" && normalized.Name == "Naledi" && normalized.Species == "Serval", "Trim and uppercase");
        foreach (string id in new[] { "", "WR-123", "WR-12345", "WR-12a4", "XX-0001", "WR-１２３４", "WR-0001extra" })
        {
            CheckInvalid(id, "Name", "Species", "1", "1", "Animal ID");
        }
        foreach (string name in new[] { "", "   ", new string('a', 31), "A|B", "A\nB", "A\rB" })
        {
            CheckInvalid("WR-0001", name, "Species", "1", "1", "Name");
            CheckInvalid("WR-0001", "Name", name, "1", "1", "Species");
        }
        foreach (string number in new[] { "", " ", "abc", "1.5", "1.0", "1,5", "-1", "101", "9999999999999999999999", "1e1" })
        {
            CheckInvalid("WR-0001", "Name", "Species", number, "1", "Age");
            CheckInvalid("WR-0001", "Name", "Species", "1", number, "Recovery Score");
        }
        Check(AnimalValidator.TryCreateAnimal("WR-9999", new string('a', 30), new string('b', 30), "100", "0", out _, out _, out _), "Maximum lengths and opposite numeric endpoints");
    }

    // Confirms invalid input reports the exact field and never produces a savable object.
    private static void CheckInvalid(string id, string name, string species, string age, string score, string expectedField)
    {
        bool valid = AnimalValidator.TryCreateAnimal(id, name, species, age, score, out Animal? animal, out string field, out string message);
        Check(!valid && animal == null && field == expectedField && message.Contains(expectedField), "Reject invalid " + expectedField);
    }

    // Verifies UTF-8 append preservation, duplicate protection, malformed input, and file failures.
    private static void CheckFiles()
    {
        string firstRecord = "WR-0001|Thabo|African Penguin|4|85|Release-Ready|Pre-Release Camp";
        int caseNumber = 0;
        foreach (string ending in new[] { "", "\n", "\r\n", "\r" })
        {
            foreach (bool bom in new[] { false, true })
            {
                string path = Path.Combine(testFolder, "append-" + caseNumber++ + ".txt");
                File.WriteAllText(path, firstRecord + ending, new UTF8Encoding(bom));
                byte[] before = File.ReadAllBytes(path);
                FileHandler handler = new FileHandler(path);
                Animal next = CreateAnimal("WR-0002", 19);
                next.Name = "Léa";
                Check(handler.TryAppendAnimal(next, out bool duplicate, out _) && !duplicate, "Append case " + caseNumber);
                byte[] after = File.ReadAllBytes(path);
                Check(after.Take(before.Length).SequenceEqual(before), "Preserve every pre-existing byte " + caseNumber);
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                Check(lines.Length == 2 && lines[0] == firstRecord && lines[1] == "WR-0002|Léa|Serval|3|19|Critical|Intensive Care Unit", "Exact UTF-8 file format " + caseNumber);
                Check(!handler.TryAppendAnimal(next, out duplicate, out _) && duplicate, "Reject duplicate " + caseNumber);
                Check(File.ReadAllBytes(path).SequenceEqual(after), "Duplicate leaves file unchanged " + caseNumber);
            }
        }

        string emptyPath = Path.Combine(testFolder, "empty.txt");
        FileHandler empty = new FileHandler(emptyPath);
        Check(empty.TryLoadAnimals(out List<Animal> records, out int skipped, out _) && records.Count == 0 && skipped == 0 && File.Exists(emptyPath), "Missing file creates empty grid source");
        Check(empty.TryAppendAnimal(CreateAnimal("WR-0010", 20), out _, out _), "Append to empty file");
        Check(empty.TryLoadAnimals(out records, out skipped, out _) && records.Count == 1 && skipped == 0, "Reload appended record");
        Check(empty.TryLoadAnimals(out records, out skipped, out _) && records.Count == 1, "Repeated reload has no duplicate rows");
        string newPath = Path.Combine(testFolder, "new-on-add.txt");
        Check(new FileHandler(newPath).TryAppendAnimal(CreateAnimal("WR-0011", 59), out _, out _) && File.ReadAllLines(newPath).Length == 1, "Add can create a missing file");
        string bomPath = Path.Combine(testFolder, "bom-only.txt");
        File.WriteAllBytes(bomPath, Encoding.UTF8.GetPreamble());
        Check(new FileHandler(bomPath).TryAppendAnimal(CreateAnimal("WR-0012", 60), out _, out _) && File.ReadAllLines(bomPath).Length == 1, "BOM-only file gets no leading blank line");

        string malformedPath = Path.Combine(testFolder, "malformed.txt");
        File.WriteAllLines(malformedPath, new[] { firstRecord, "", "bad", "WR-0003|Name|Species|x|50|Stable|Recovery Ward", "WR-0004|Name|Species|3|no|Stable|Recovery Ward", "BAD|Name|Species|3|50|Stable|Recovery Ward", " wr-0042 |broken", "WR-0005|Name|Species|3|50|Serious|Recovery Ward", "WR-0006|Name|Species|3|50|Stable|Recovery Ward" });
        FileHandler malformed = new FileHandler(malformedPath);
        Check(malformed.TryLoadAnimals(out records, out skipped, out _) && records.Count == 2 && skipped == 7, "Skip malformed lines and count once");
        Check(records[0].Id == "WR-0001" && records[1].Id == "WR-0006", "Maintain file order");
        byte[] malformedBefore = File.ReadAllBytes(malformedPath);
        Check(!malformed.TryAppendAnimal(CreateAnimal("WR-0042", 20), out bool isDuplicate, out _) && isDuplicate, "Duplicate check is case-insensitive even on malformed records");
        Check(File.ReadAllBytes(malformedPath).SequenceEqual(malformedBefore), "Malformed duplicate does not modify file");

        byte[] emptyBefore = File.ReadAllBytes(emptyPath);
        using (FileStream locked = new FileStream(emptyPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
        {
            Check(!empty.TryAppendAnimal(CreateAnimal("WR-0090", 40), out _, out string message) && message.Contains("locking"), "Friendly locked-file append failure");
            Check(!empty.TryLoadAnimals(out records, out _, out message) && records.Count == 0 && message.Contains("locking"), "Friendly locked-file load failure");
        }
        Check(File.ReadAllBytes(emptyPath).SequenceEqual(emptyBefore), "Locked file stays unchanged");
        File.SetAttributes(emptyPath, FileAttributes.ReadOnly);
        try
        {
            Check(!empty.TryAppendAnimal(CreateAnimal("WR-0091", 40), out _, out string message) && message.Contains("read-only"), "Friendly read-only failure");
        }
        finally
        {
            File.SetAttributes(emptyPath, FileAttributes.Normal);
        }
        Check(File.ReadAllBytes(emptyPath).SequenceEqual(emptyBefore), "Read-only file stays unchanged");
        FileHandler missingFolder = new FileHandler(Path.Combine(testFolder, "missing", "animals.txt"));
        Check(!missingFolder.TryAppendAnimal(CreateAnimal("WR-0092", 40), out _, out string folderMessage) && folderMessage.Contains("folder"), "Friendly missing-folder append failure");
        Check(!missingFolder.TryLoadAnimals(out _, out _, out folderMessage) && folderMessage.Contains("folder"), "Friendly missing-folder load failure");
        CultureInfo previousCulture = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
            Check(FileHandler.ToFileLine(CreateAnimal("WR-0093", 80)) == "WR-0093|Naledi|Serval|3|80|Release-Ready|Pre-Release Camp", "File format is culture independent");
        }
        finally
        {
            CultureInfo.CurrentCulture = previousCulture;
        }
    }

    // Tests the real form's controls and event handlers in the checks executable's own output folder.
    private static void CheckForm(string? imagePath)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "animals.txt");
        byte[]? original = File.Exists(path) ? File.ReadAllBytes(path) : null;
        try
        {
            File.WriteAllText(path, string.Empty);
            Application.EnableVisualStyles();
            using (MainForm form = new MainForm())
            {
                form.ShowInTaskbar = false;
                form.Show();
                Application.DoEvents();
                TextBox id = (TextBox)form.Controls.Find("txtAnimalId", true)[0];
                TextBox name = (TextBox)form.Controls.Find("txtName", true)[0];
                TextBox species = (TextBox)form.Controls.Find("txtSpecies", true)[0];
                TextBox age = (TextBox)form.Controls.Find("txtAge", true)[0];
                TextBox score = (TextBox)form.Controls.Find("txtRecoveryScore", true)[0];
                Button add = (Button)form.Controls.Find("btnAdd", true)[0];
                DataGridView grid = (DataGridView)form.Controls.Find("dgvAnimals", true)[0];
                Label feedback = (Label)form.Controls.Find("lblFeedback", true)[0];
                Label classification = (Label)form.Controls.Find("lblClassification", true)[0];
                Check(grid.ReadOnly && !grid.AllowUserToAddRows && !grid.AllowUserToDeleteRows && grid.SelectionMode == DataGridViewSelectionMode.FullRowSelect, "Read-only full-row grid");
                Check(string.Join("|", grid.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText)) == "Animal ID|Name|Species|Age|Recovery Score|Status|Housing Unit", "Exact column headers and order");
                add.PerformClick();
                Check(feedback.Text.Contains("Animal ID") && id.Focused && new FileInfo(path).Length == 0, "Empty Add shows field error and focuses ID");
                id.Text = " wr-0007 "; name.Text = " Naledi "; species.Text = " Serval "; age.Text = "3"; score.Text = "79.5";
                add.PerformClick();
                Check(score.Focused && feedback.Text.Contains("Recovery Score") && new FileInfo(path).Length == 0, "Bad score rejected by Add UI");
                score.Text = "79";
                add.PerformClick();
                Application.DoEvents();
                Check(grid.Rows.Count == 1 && File.ReadAllLines(path).Length == 1, "Add saves one record and refreshes grid");
                Check(id.Text == "" && name.Text == "" && species.Text == "" && age.Text == "" && score.Text == "" && id.Focused, "Successful Add clears every input and focuses ID");
                Check(classification.Text.Contains("Recovering") && classification.Text.Contains("Outdoor Enclosure"), "Successful Add displays classification");
                id.Text = "wr-0007"; name.Text = "Duplicate"; species.Text = "Serval"; age.Text = "3"; score.Text = "80";
                add.PerformClick();
                Check(id.Focused && feedback.Text.Contains("already exists") && File.ReadAllLines(path).Length == 1, "Duplicate error focuses ID without saving");
                id.Text = "WR-0008";
                File.SetAttributes(path, FileAttributes.ReadOnly);
                try
                {
                    add.PerformClick();
                    Check(feedback.Text.Contains("read-only") && id.Text == "WR-0008" && name.Text == "Duplicate" && grid.Rows.Count == 1, "Write failure preserves form inputs and grid");
                }
                finally
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                }
                name.Text = "Thabo";
                add.PerformClick();
                Application.DoEvents();
                Check(grid.Rows.Count == 2 && classification.Text.Contains("Release-Ready"), "Second Add refreshes without duplicates");
                if (imagePath != null)
                {
                    using (Bitmap image = new Bitmap(form.Width, form.Height))
                    {
                        form.DrawToBitmap(image, new Rectangle(0, 0, form.Width, form.Height));
                        image.Save(imagePath);
                    }
                }
                form.Close();
            }
        }
        finally
        {
            if (original == null)
            {
                File.Delete(path);
            }
            else
            {
                File.WriteAllBytes(path, original);
            }
        }
    }

    // Creates valid independent test data and applies the production classification method.
    private static Animal CreateAnimal(string id, int score)
    {
        Animal animal = new Animal(id, "Naledi", "Serval", 3, score);
        AnimalClassifier.Classify(animal);
        return animal;
    }

    // Stops the run at the first failed expectation with a meaningful description.
    private static void Check(bool condition, string description)
    {
        if (!condition)
        {
            throw new InvalidOperationException(description);
        }
        checks++;
    }
}
