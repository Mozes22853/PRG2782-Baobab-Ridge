using System.Reflection;
using System.Text;
using BaobabRidge.BusinessLogicLayer;
using BaobabRidge.DataLayer;
using BaobabRidge.PresentationLayer;

internal static partial class Program
{
    // Checks ordinary updates, malformed-ID collisions, ambiguous IDs and failed file operations.
    private static void CheckUpdateFiles()
    {
        string path = Path.Combine(testFolder, "update.txt");
        FileHandler handler = new FileHandler(path);
        string first = FileHandler.ToFileLine(CreateAnimal("WR-0101", 79));
        string other = FileHandler.ToFileLine(CreateAnimal("WR-0102", 42));
        string malformed = "WR-0101|Bad|Serval|letters|79|Recovering|Outdoor Enclosure";
        Animal updated = CreateAnimal("WR-0101", 80);
        File.WriteAllLines(path, new[] { malformed, first, other }, Encoding.UTF8);
        bool updateSucceeded = handler.TryUpdateAnimal(updated, out string updateMessage);
        Check(updateSucceeded, "Update finds the valid record after a malformed line with the same ID: " + updateMessage);
        string[] lines = File.ReadAllLines(path);
        Check(lines.Length == 3 && lines[0] == malformed && lines[1] == FileHandler.ToFileLine(updated) && lines[2] == other,
            "Update changes only the selected valid record, preserving unrelated and malformed lines");
        Check(handler.TryLoadAnimals(out List<Animal> animals, out int skipped, out _) && animals.Count == 2 && skipped == 1,
            "Malformed-ID update creates no duplicate animal");

        // Use two fully valid records with the same ID, differing only in ID case.
        File.WriteAllLines(path, new[] { first, "wr-0101" + first.Substring(7) });
        byte[] before = File.ReadAllBytes(path);
        Check(!handler.TryUpdateAnimal(updated, out string message) && message.Contains("More than one"), "Ambiguous valid duplicate IDs reject Update");
        Check(File.ReadAllBytes(path).SequenceEqual(before), "Ambiguous-ID failure preserves original file bytes");

        File.WriteAllLines(path, new[] { other });
        before = File.ReadAllBytes(path);
        Check(!handler.TryUpdateAnimal(updated, out message) && message.Contains("not found"), "Removed target reports not found");
        Check(File.ReadAllBytes(path).SequenceEqual(before), "Missing-target failure preserves original bytes");
        File.WriteAllLines(path, new[] { first, other });
        before = File.ReadAllBytes(path);
        File.SetAttributes(path, FileAttributes.ReadOnly);
        try
        {
            Check(!handler.TryUpdateAnimal(updated, out message) && message.Contains("permissions"), "Read-only Update reports friendly permission error");
            Check(File.ReadAllBytes(path).SequenceEqual(before), "Read-only Update preserves original bytes");
        }
        finally
        {
            File.SetAttributes(path, FileAttributes.Normal);
        }
        using (FileStream locked = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
        {
            Check(!handler.TryUpdateAnimal(updated, out message) && message.Length > 0, "Locked Update returns friendly error");
        }
        Check(File.ReadAllBytes(path).SequenceEqual(before), "Locked Update preserves original bytes");
        Check(Directory.GetFiles(testFolder, "update.txt.*.tmp").Length == 0, "Update cleans up temporary files on failure");
        Check(!new FileHandler(Path.Combine(testFolder, "absent.txt")).TryUpdateAnimal(updated, out message) && message.Length > 0,
            "Missing-file Update returns friendly error");
        Check(!new FileHandler(Path.Combine(testFolder, "absent", "animals.txt")).TryUpdateAnimal(updated, out message) && message.Length > 0,
            "Missing-folder Update returns friendly error");
    }

    // Exercises the actual Search, row-selection, Save Changes and Clear events on disposable records.
    private static void CheckUpdateForm(string? imagePath)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "animals.txt");
        byte[]? original = File.Exists(path) ? File.ReadAllBytes(path) : null;
        string first = FileHandler.ToFileLine(CreateAnimal("WR-0101", 79));
        string other = FileHandler.ToFileLine(CreateAnimal("WR-0102", 42));
        try
        {
            File.WriteAllLines(path, new[] { first, other });
            using (MainForm form = new MainForm())
            {
                form.ShowInTaskbar = false;
                form.Show();
                Application.DoEvents();
                TextBox id = (TextBox)form.Controls.Find("txtAnimalId", true)[0];
                TextBox name = (TextBox)form.Controls.Find("txtName", true)[0];
                TextBox score = (TextBox)form.Controls.Find("txtRecoveryScore", true)[0];
                TextBox searchId = (TextBox)form.Controls.Find("txtSearchId", true)[0];
                Button search = (Button)form.Controls.Find("btnSearch", true)[0];
                Button save = (Button)form.Controls.Find("btnSaveChanges", true)[0];
                Button clear = (Button)form.Controls.Find("btnClear", true)[0];
                Button add = (Button)form.Controls.Find("btnAdd", true)[0];
                DataGridView grid = (DataGridView)form.Controls.Find("dgvAnimals", true)[0];
                Label feedback = (Label)form.Controls.Find("lblFeedback", true)[0];
                Check(!save.Enabled && add.Enabled && !id.ReadOnly && form.AcceptButton == add, "Startup Add mode disables Save and permits a new ID");
                Check(grid.Rows.Count == 2 && grid.SelectedRows.Count == 0, "Integrated View loads records without accidental selection");
                search.PerformClick();
                Check(searchId.Focused && feedback.Text.Contains("search box"), "Empty search gives feedback and focuses search field");
                searchId.Text = " wr-0101 ";
                search.PerformClick();
                Check(id.Text == "WR-0101" && name.Text == "Naledi" && score.Text == "79", "Case-insensitive exact search loads the correct animal");
                Check(id.ReadOnly && !add.Enabled && save.Enabled && form.AcceptButton == save, "Edit mode locks ID, disables Add and enables Save");
                searchId.Text = "WR-01";
                search.PerformClick();
                Check(id.Text == "WR-0101" && score.Text == "79" && feedback.Text.Contains("not found"), "Partial-ID search leaves the loaded record unchanged");
                searchId.Text = "WR-9999";
                search.PerformClick();
                Check(id.Text == "WR-0101" && save.Enabled && feedback.Text.Contains("not found"), "Unknown search preserves Edit mode and inputs");
                byte[] before = File.ReadAllBytes(path);
                score.Text = "79.5";
                save.PerformClick();
                Check(score.Focused && feedback.Text.Contains("Recovery Score") && File.ReadAllBytes(path).SequenceEqual(before), "Invalid Update score focuses the field without writing");
                score.Text = "80";
                File.SetAttributes(path, FileAttributes.ReadOnly);
                try
                {
                    save.PerformClick();
                    Check(save.Enabled && id.ReadOnly && score.Text == "80" && feedback.Text.Contains("permissions"), "Failed Update retains Edit mode and unsaved input");
                }
                finally
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                }
                save.PerformClick();
                Application.DoEvents();
                string[] lines = File.ReadAllLines(path);
                Check(lines.Length == 2 && lines[0].EndsWith("|80|Release-Ready|Pre-Release Camp") && lines[1] == other,
                    "UI Save crosses 79-to-80 boundary and changes only the selected record");
                Check(id.Text == "" && name.Text == "" && score.Text == "" && id.Focused && !id.ReadOnly && add.Enabled && !save.Enabled,
                    "Successful Update clears fields, restores Add mode and focuses ID");
                Check(grid.Rows.Count == 2 && ((Animal)grid.Rows[0].DataBoundItem!).RecoveryScore == 80, "Update refreshes the grid without duplicate rows");
                // Raise the grid's normal cell-click event to exercise its connected handler.
                typeof(DataGridView).GetMethod("OnCellClick", BindingFlags.Instance | BindingFlags.NonPublic)!
                    .Invoke(grid, new object[] { new DataGridViewCellEventArgs(0, 1) });
                Check(id.Text == "WR-0102" && id.ReadOnly && !add.Enabled && save.Enabled, "Row click loads the same input boxes in Edit mode");
                clear.PerformClick();
                Check(id.Text == "" && name.Text == "" && searchId.Text == "" && !save.Enabled && add.Enabled && !id.ReadOnly && id.Focused,
                    "Clear empties inputs and search, restores Add mode and focuses ID");
                Check(grid.SelectedRows.Count == 0 && grid.CurrentCell == null, "Clear removes grid selection");
                before = File.ReadAllBytes(path);
                save.PerformClick();
                Check(File.ReadAllBytes(path).SequenceEqual(before), "Disabled Save cannot modify a record in Add mode");
                foreach (Control control in new Control[] { id, name, score, searchId, search, save, clear, add, grid })
                {
                    Check(control.Parent == form && control.Dock == DockStyle.None && form.ClientRectangle.Contains(control.Bounds),
                        control.Name + " is directly editable on the form and fully visible");
                }
                Check(!add.Bounds.IntersectsWith(save.Bounds) && !clear.Bounds.IntersectsWith(save.Bounds)
                    && !search.Bounds.IntersectsWith(grid.Bounds), "Action buttons do not overlap each other or the grid");
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
            File.SetAttributes(path, FileAttributes.Normal);
            if (original == null) File.Delete(path);
            else File.WriteAllBytes(path, original);
        }
    }
}
