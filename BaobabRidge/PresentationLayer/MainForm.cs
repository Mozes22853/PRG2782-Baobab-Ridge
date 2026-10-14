using BaobabRidge.BusinessLogicLayer;
using BaobabRidge.DataLayer;

namespace BaobabRidge.PresentationLayer
{
    public partial class MainForm : Form
    {
        private readonly FileHandler fileHandler;

        // Creates the form and the handler for the file beside the executable.
        public MainForm()
        {
            InitializeComponent();
            fileHandler = new FileHandler();
        }

        // Loads the current file when the application opens.
        private void MainForm_Load(object? sender, EventArgs e)
        {
            RefreshAnimals();
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
        //Member3 Tracks the ID currently being edited in Edit Mode
        private string? currentEditingId = null;

        // Feature 3.1: Load record for editing when clicking a row in the grid
        private void dgvAnimals_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvAnimals.Rows.Count)
            {
                if (dgvAnimals.Rows[e.RowIndex].DataBoundItem is Animal selectedAnimal)
                {
                    currentEditingId = selectedAnimal.Id;
                    txtAnimalId.Text = selectedAnimal.Id;
                    txtName.Text = selectedAnimal.Name;
                    txtSpecies.Text = selectedAnimal.Species;
                    txtAge.Text = selectedAnimal.Age.ToString();
                    txtRecoveryScore.Text = selectedAnimal.RecoveryScore.ToString();

                    SetEditMode(true);
                }
            }
        }

        // Feature 3.1: Search by exact Animal ID (case-insensitive)[cite: 4]
        private void btnSearch_Click(object? sender, EventArgs e)
        {
            string searchId = txtAnimalId.Text.Trim();
            if (string.IsNullOrEmpty(searchId))
            {
                ShowFeedback("Enter an Animal ID to search.", true);
                return;
            }

            if (!fileHandler.TryLoadAnimals(out List<Animal> animals, out _, out string message))
            {
                ShowFeedback(message, true);
                return;
            }

            Animal? found = animals.FirstOrDefault(a => string.Equals(a.Id, searchId, StringComparison.OrdinalIgnoreCase));
            if (found != null)
            {
                currentEditingId = found.Id;
                txtAnimalId.Text = found.Id;
                txtName.Text = found.Name;
                txtSpecies.Text = found.Species;
                txtAge.Text = found.Age.ToString();
                txtRecoveryScore.Text = found.RecoveryScore.ToString();

                SetEditMode(true);
                ShowFeedback("Animal loaded for editing.", false);
            }
            else
            {
                ShowFeedback($"Animal ID '{searchId}' was not found.", true);
            }
        }

        // Feature 3.2: Manage form states (Edit Mode vs. Add Mode)[cite: 4]
        private void SetEditMode(bool isEditing)
        {
            if (isEditing)
            {
                txtAnimalId.Enabled = false;   // ID cannot be changed[cite: 4]
                btnAdd.Enabled = false;        // Disable Add button[cite: 4]
            }
            else
            {
                currentEditingId = null;
                ClearInputs();
                txtAnimalId.Enabled = true;
                btnAdd.Enabled = true;
            }
        }

        // Feature 3.3 & 3.4: Validate, recalculate score boundaries, and rewrite file precisely[cite: 4]
        private void btnSaveChanges_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentEditingId))
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

            // Recalculate Status and Housing Unit from recovery score (even if unchanged)[cite: 4]
            AnimalClassifier.Classify(updatedAnimal!);

            // Save via precision file rewriting
            if (!fileHandler.TryUpdateAnimal(updatedAnimal!, out message))
            {
                ShowFeedback(message, true);
                return;
            }

            SetEditMode(false);
            lblClassification.Text = "Updated " + updatedAnimal!.Id + Environment.NewLine
                + "Status: " + updatedAnimal.Status + Environment.NewLine
                + "Housing Unit: " + updatedAnimal.HousingUnit;
            ShowFeedback("Animal updated successfully.", false);
            RefreshAnimals();
        }

        // Clear button leaves edit mode and returns form to Add mode[cite: 4] end of Member3
        private void btnClear_Click(object? sender, EventArgs e)
        {
            SetEditMode(false);
            ShowFeedback("Cleared form. Returned to Add mode.", false);
        }

        // Replaces the grid data without accumulating rows; Member 2 can extend this integration point.
        private void RefreshAnimals()
        {
            if (!fileHandler.TryLoadAnimals(out List<Animal> animals, out int skippedLines, out string message))
            {
                lblLoadNotice.Text = "View could not be refreshed. " + message;
                return;
            }
            dgvAnimals.DataSource = animals;
            lblLoadNotice.Text = animals.Count + " animal(s) loaded."
                + (skippedLines > 0 ? " Skipped " + skippedLines + " malformed line(s)." : string.Empty);
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
