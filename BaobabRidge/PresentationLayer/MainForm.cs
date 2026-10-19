using BaobabRidge.BusinessLogicLayer;
using BaobabRidge.DataLayer;

namespace BaobabRidge.PresentationLayer
{
    public partial class MainForm : Form
    {
        private readonly FileHandler fileHandler;
        private string? currentEditingId;

        // Creates the form and the handler for the file beside the executable.
        public MainForm()
        {
            InitializeComponent();
            fileHandler = new FileHandler();
            ConfigureGrid();
            InitializeMember4Controls();
            SetEditMode(false);
        }

        // Defines the seven grid columns explicitly so headers and order match the brief
        // (Animal ID, Name, Species, Age, Recovery Score, Status, Housing Unit).
        private void ConfigureGrid()
        {
            dgvAnimals.AutoGenerateColumns = false;
            dgvAnimals.Columns.Clear();

            colId = CreateGridColumn("colId", "Animal ID", nameof(Animal.Id));
            colName = CreateGridColumn("colName", "Name", nameof(Animal.Name));
            colSpecies = CreateGridColumn("colSpecies", "Species", nameof(Animal.Species));
            colAge = CreateGridColumn("colAge", "Age", nameof(Animal.Age));
            colScore = CreateGridColumn("colScore", "Recovery Score", nameof(Animal.RecoveryScore));
            colStatus = CreateGridColumn("colStatus", "Status", nameof(Animal.Status));
            colHousing = CreateGridColumn("colHousing", "Housing Unit", nameof(Animal.HousingUnit));

            dgvAnimals.Columns.AddRange(colId, colName, colSpecies, colAge, colScore, colStatus, colHousing);

            // The grid only displays data: no editing, adding, deleting or reordering by the user.
            dgvAnimals.ReadOnly = true;
            dgvAnimals.AllowUserToAddRows = false;
            dgvAnimals.AllowUserToDeleteRows = false;
            dgvAnimals.AllowUserToOrderColumns = false;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.MultiSelect = false;
        }

        // Builds one read-only, non-sortable text column bound to a property of Animal.
        private static DataGridViewTextBoxColumn CreateGridColumn(string name, string header, string propertyName)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = propertyName,
                ReadOnly = true,
                // Sorting is off so records always stay in the same order as the file.
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
        }

        // Loads the current file when the application opens.
        private void MainForm_Load(object? sender, EventArgs e)
        {
            RefreshAnimals(true);
            txtAnimalId.Focus();
        }

        // Validates, classifies, and saves one animal before refreshing the view.
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!AnimalValidator.TryCreateAnimal(txtAnimalId.Text, txtName.Text, txtSpecies.Text,
                txtAge.Text, txtRecoveryScore.Text, out Animal? animal,
                out string invalidField, out string message))
            {
                ShowFeedback(message, true);
                FocusInvalidField(invalidField);
                return;
            }

            AnimalClassifier.Classify(animal!);
            if (!fileHandler.TryAppendAnimal(animal!, out bool duplicateId, out message))
            {
                ShowFeedback(message, true);
                if (duplicateId)
                {
                    FocusInvalidField("Animal ID");
                }
                return;
            }

            // A successful append stays successful even if a later refresh cannot read the file.
            ClearInputs();
            lblClassification.Text = "Saved " + animal!.Id + Environment.NewLine
                + "Status: " + animal.Status + Environment.NewLine
                + "Housing Unit: " + animal.HousingUnit;
            ShowFeedback("Animal added successfully. Ready for the next animal.", false);
            RefreshAnimals();
        }

        // Loads a selected animal into the same input boxes used by Add.
        private void dgvAnimals_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvAnimals.Rows.Count
                && dgvAnimals.Rows[e.RowIndex].DataBoundItem is Animal animal)
            {
                LoadAnimalForEditing(animal);
            }
        }

        // Searches the current file using an exact, case-insensitive ID match.
        private void btnSearch_Click(object? sender, EventArgs e)
        {
            string searchId = txtSearchId.Text.Trim();
            if (searchId.Length == 0)
            {
                ShowFeedback("Enter an Animal ID in the search box.", true);
                txtSearchId.Focus();
                return;
            }
            if (!fileHandler.TryLoadAnimals(out List<Animal> animals, out _, out string message))
            {
                ShowFeedback(message, true);
                return;
            }
            foreach (Animal animal in animals)
            {
                if (string.Equals(animal.Id, searchId, StringComparison.OrdinalIgnoreCase))
                {
                    LoadAnimalForEditing(animal);
                    return;
                }
            }
            ShowFeedback("Animal ID '" + searchId + "' was not found.", true);
        }

        // Gives row selection and ID searching the same edit-mode behaviour.
        private void LoadAnimalForEditing(Animal animal)
        {
            currentEditingId = animal.Id;
            txtAnimalId.Text = animal.Id;
            txtName.Text = animal.Name;
            txtSpecies.Text = animal.Species;
            txtAge.Text = animal.Age.ToString(System.Globalization.CultureInfo.InvariantCulture);
            txtRecoveryScore.Text = animal.RecoveryScore.ToString(System.Globalization.CultureInfo.InvariantCulture);
            SetEditMode(true);
            ShowFeedback("Animal loaded for editing. Change the details and select Save Changes.", false);
            txtName.Focus();
        }

        // Keeps the immutable ID and action buttons consistent with the current form mode.
        private void SetEditMode(bool isEditing)
        {
            txtAnimalId.ReadOnly = isEditing;
            btnAdd.Enabled = !isEditing;
            btnSaveChanges.Enabled = isEditing;
            lblMode.Text = isEditing ? "Edit mode: " + currentEditingId : "Add mode";
            AcceptButton = isEditing ? btnSaveChanges : btnAdd;
            if (!isEditing)
            {
                currentEditingId = null;
                ClearInputs();
            }
        }

        // Validates and reclassifies the loaded animal before updating its single file record.
        private void btnSaveChanges_Click(object? sender, EventArgs e)
        {
            if (currentEditingId == null)
            {
                ShowFeedback("No animal is currently loaded for updating.", true);
                return;
            }
            if (!AnimalValidator.TryCreateAnimal(currentEditingId, txtName.Text, txtSpecies.Text,
                txtAge.Text, txtRecoveryScore.Text, out Animal? updatedAnimal,
                out string invalidField, out string message))
            {
                ShowFeedback(message, true);
                FocusInvalidField(invalidField);
                return;
            }
            AnimalClassifier.Classify(updatedAnimal!);
            if (!fileHandler.TryUpdateAnimal(updatedAnimal!, out message))
            {
                ShowFeedback(message, true);
                return;
            }
            SetEditMode(false);
            lblClassification.Text = "Updated " + updatedAnimal!.Id + Environment.NewLine
                + "Status: " + updatedAnimal.Status + Environment.NewLine
                + "Housing Unit: " + updatedAnimal.HousingUnit;
            ShowFeedback("Animal updated successfully. Ready for the next animal.", false);
            RefreshAnimals();
        }

        // Leaves edit mode and clears the form for a new animal.
        private void btnClear_Click(object? sender, EventArgs e)
        {
            SetEditMode(false);
            txtSearchId.Clear();
            dgvAnimals.ClearSelection();
            dgvAnimals.CurrentCell = null;
            lblClassification.Text = "No animal selected.";
            ShowFeedback("Cleared form. Returned to Add mode.", false);
        }

        // Reloads animals.txt into the grid, replacing (never adding to) the previous rows.
        // When announceProblems is true (application start) the user is told once about skipped lines
        // or a file error; later refreshes only update the status label so no repeated pop-ups appear.
        private void RefreshAnimals(bool announceProblems = false)
        {
            if (!fileHandler.TryLoadAnimals(out List<Animal> animals, out int skippedLines, out string message))
            {
                lblLoadNotice.Text = "View could not be refreshed. " + message;
                if (announceProblems)
                {
                    // Show an empty grid rather than leaving a half-initialised one on screen.
                    dgvAnimals.DataSource = new List<Animal>();
                    MessageBox.Show(message, "Baobab Ridge - file problem",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            dgvAnimals.DataSource = animals;
            // Binding selects the first row automatically; clear it so nothing is "selected" by accident.
            dgvAnimals.ClearSelection();
            dgvAnimals.CurrentCell = null;

            lblLoadNotice.Text = animals.Count + " animal(s) loaded."
                + (skippedLines > 0 ? " Skipped " + skippedLines + " malformed line(s)." : string.Empty);

            if (announceProblems && skippedLines > 0)
            {
                MessageBox.Show(skippedLines + " malformed line(s) in animals.txt were skipped and are not shown.",
                    "Baobab Ridge - skipped lines", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Clears only editable input boxes after a successful save.
        private void ClearInputs()
        {
            txtAnimalId.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtRecoveryScore.Clear();
            txtAnimalId.Focus();
        }

        // Shows a persistent, readable success or error message without a stack trace.
        private void ShowFeedback(string message, bool isError)
        {
            lblFeedback.ForeColor = isError ? Color.Maroon : Color.DarkGreen;
            lblFeedback.Text = message;
        }

        // Returns the cursor to the first invalid field so the user can correct it.
        private void FocusInvalidField(string field)
        {
            TextBox input = txtAnimalId;
            switch (field)
            {
                case "Name":
                    input = txtName;
                    break;
                case "Species":
                    input = txtSpecies;
                    break;
                case "Age":
                    input = txtAge;
                    break;
                case "Recovery Score":
                    input = txtRecoveryScore;
                    break;
            }
            input.Focus();
            input.SelectAll();
        }
    }
}
